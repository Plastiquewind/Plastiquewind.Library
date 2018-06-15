using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Plastiquewind.Base.Abstractions;
using Plastiquewind.Base.Abstractions.Errors;
using Plastiquewind.Base.Implementations;
using Plastiquewind.Parsers.Abstractions;
using Plastiquewind.Parsers.Helpers;
using Plastiquewind.Parsers.Implementations.Errors;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml;
using PommaLabs.Thrower;

namespace Plastiquewind.Parsers.Implementations
{
    public class XlsxParser<T> : IParser<IXlsxParsedRow<T>>
    {
        private readonly IXlsxParserConfigFactory<T> configFactory;

        public XlsxParser(IXlsxParserConfigFactory<T> configFactory)
        {
            Raise.ArgumentNullException.IfIsNull(configFactory, nameof(configFactory));

            this.configFactory = configFactory;
        }

        public virtual IProcessingResult<IEnumerable<IXlsxParsedRow<T>>> Parse(Stream stream)
        {
            Raise.ArgumentNullException.IfIsNull(stream, nameof(stream));

            var parsedEntities = new List<IXlsxParsedRow<T>>();
            var errors = new List<IError>();

            IProcessingResult<IXlsxParserConfig<T>> configBuildingResult = configFactory.Create(stream);

            if (configBuildingResult.Errors != null && configBuildingResult.Errors.Any())
            {
                return new ProcessingResult<IEnumerable<IXlsxParsedRow<T>>>(Enumerable.Empty<IXlsxParsedRow<T>>(), configBuildingResult.Errors);
            }

            IXlsxParserConfig<T> config = configBuildingResult.Result;
            var firstRow = config.FirstRow;
            var lastRow = config.LastRow;
            var firstColumn = XlsxColumnAddressConverter.ToInt(config.FirstColumn);
            var lastColumn = XlsxColumnAddressConverter.ToInt(config.LastColumn);
            var firstSheet = config.FirstSheet;
            var lastSheet = config.LastSheet;
            var fieldsMap = config.FieldsMap;
            var typeAccessor = config.TypeAccessor;
            var valueConverter = config.ValueConverter;

            using (var spreadsheetDocument = SpreadsheetDocument.Open(stream, false))
            {
                WorkbookPart workbookPart = spreadsheetDocument.WorkbookPart;
                int currentWorksheetIndex = 0;
                string[] sharedStrings = workbookPart.GetPartsOfType<SharedStringTablePart>()
                    .FirstOrDefault()?
                    .SharedStringTable?
                    .Select(x => x.InnerText)?
                    .ToArray();

                foreach (WorksheetPart worksheetPart in workbookPart.WorksheetParts)
                {
                    if (currentWorksheetIndex < firstSheet)
                    {
                        continue;
                    }

                    string currentSheetName = workbookPart.Workbook.Descendants<Sheet>().ElementAt(currentWorksheetIndex).Name;

                    using (OpenXmlReader reader = OpenXmlReader.Create(worksheetPart))
                    {
                        while (reader.Read())
                        {
                            if (reader.ElementType != typeof(Row))
                            {
                                continue;
                            }

                            do
                            {
                                if (!reader.HasAttributes || reader.ElementType != typeof(Row))
                                {
                                    continue;
                                }

                                var rowNumber = int.Parse(reader.Attributes.First(a => a.LocalName == "r").Value);

                                if (rowNumber < firstRow)
                                {
                                    continue;
                                }

                                if (rowNumber > lastRow)
                                {
                                    break;
                                }

                                var entity = (T)Activator.CreateInstance(typeof(T));
                                bool entityHasErrors = false;

                                while (reader.Read())
                                {
                                    if (reader.ElementType == typeof(Row))
                                    {
                                        break;
                                    }

                                    if (reader.ElementType != typeof(Cell) && 
                                        reader.ElementType != typeof(CellValue) ||
                                        !reader.HasAttributes)
                                    {
                                        continue;
                                    }

                                    string cellAddress = reader.Attributes.First(a => a.LocalName == "r").Value;
                                    string column = cellAddress.Replace(rowNumber.ToString(), string.Empty);
                                    var columnNumber = XlsxColumnAddressConverter.ToInt(column);

                                    if (columnNumber < firstColumn)
                                    {
                                        continue;
                                    }

                                    if (columnNumber > lastColumn)
                                    {
                                        break;
                                    }

                                    if (!fieldsMap.Has(column))
                                    {
                                        continue;
                                    }

                                    IEntityField<T> entityField = fieldsMap[column];
                                    Cell cell = (Cell)reader.LoadCurrentElement();

                                    string rawValue = cell.InnerText;

                                    if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
                                    {
                                        rawValue = sharedStrings[int.Parse(rawValue)];
                                    }
                                    else if (cell.CellFormula != null)
                                    {
                                        if (cell.CellValue == null)
                                        {
                                            errors.Add(new XlsxProtectedViewError());
                                            entityHasErrors = true;

                                            return new ProcessingResult<IEnumerable<IXlsxParsedRow<T>>>(Enumerable.Empty<IXlsxParsedRow<T>>(), new[] { new XlsxProtectedViewError() });
                                        }

                                        rawValue = cell.CellValue?.InnerText;
                                    }

                                    if (cell.DataType != null && cell.DataType.Value == CellValues.Error)
                                    {
                                        if (entityField.Type.GetTypeInfo().IsValueType ? Activator.CreateInstance(entityField.Type) != null : false)
                                        {
                                            errors.Add(new XlsxCellValueError(entityField.Description, cellAddress, currentSheetName, cell.CellValue.InnerText));
                                            entityHasErrors = true;
                                        }
                                        else
                                        {
                                            rawValue = string.Empty;
                                        }
                                    }

                                    if (valueConverter.TryConvert(rawValue, entityField.Type, out object value))
                                    {
                                        typeAccessor[entity, entityField.Name] = value;
                                    }
                                    else
                                    {
                                        errors.Add(new XlsxDataTypeError(entityField.Description, cellAddress, currentSheetName));
                                        entityHasErrors = true;
                                    }
                                }

                                if (!entityHasErrors)
                                {
                                    parsedEntities.Add(new XlsxParsedRow<T>(rowNumber, currentSheetName, entity));
                                }
                            } while (reader.ReadNextSibling());
                        }
                    }

                    if (currentWorksheetIndex == lastSheet)
                    {
                        break;
                    }

                    currentWorksheetIndex++;
                }
            }

            return new ProcessingResult<IEnumerable<IXlsxParsedRow<T>>>(parsedEntities, errors);
        }
    }
}
