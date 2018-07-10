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

            using (var spreadsheetDocument = SpreadsheetDocument.Open(stream, false))
            {
                IProcessingResult<IXlsxParserConfig<T>> configBuildingResult = configFactory.Create(spreadsheetDocument);

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
                WorkbookPart workbookPart = spreadsheetDocument.WorkbookPart;
                SharedStringTable sharedStringTable = workbookPart.GetPartsOfType<SharedStringTablePart>()
                    .FirstOrDefault()?
                    .SharedStringTable;
                string[] sharedStrings = sharedStringTable?
                    .Select(x => x.InnerText)?
                    .ToArray();

                sharedStringTable = null;

                var sheets = workbookPart.Workbook.Descendants<Sheet>();
                int sheetIndex = 1;

                foreach (var sheet in sheets)
                {
                    if (sheetIndex < firstSheet)
                    {
                        sheetIndex++;

                        continue;
                    }

                    string currentSheetName = sheet.Name;
                    WorksheetPart worksheetPart = (WorksheetPart)workbookPart.GetPartById(sheet.Id);
                    Worksheet worksheet = worksheetPart.Worksheet;
                    SheetData sheetData = worksheet.GetFirstChild<SheetData>();
                    var rows = sheetData.Elements<Row>();
                    var rowIndex = 0;

                    foreach (var row in rows)
                    {
                        var rowNumber = rowIndex + 1;

                        if (rowNumber < firstRow)
                        {
                            rowIndex++;

                            continue;
                        }

                        T entity = (T)Activator.CreateInstance(typeof(T));
                        bool entityHasErrors = false;

                        var cells = row.Elements<Cell>();
                        var cellIndex = 0;

                        foreach (var cell in cells)
                        {
                            string cellAddress;
                            int columnNumber;
                            string column;

                            if (!string.IsNullOrEmpty(cell.CellReference))
                            {
                                cellAddress = cell.CellReference;
                                column = cellAddress.Replace(rowNumber.ToString(), string.Empty);
                                columnNumber = XlsxColumnAddressConverter.ToInt(column);
                            }
                            else
                            {
                                columnNumber = cellIndex + 1;
                                column = XlsxColumnAddressConverter.ToString(columnNumber);
                                cellAddress = $"{column}{rowNumber}";
                            }

                            if (columnNumber < firstColumn)
                            {
                                cellIndex++;

                                continue;
                            }

                            if (!fieldsMap.Has(column))
                            {
                                cellIndex++;

                                continue;
                            }

                            IEntityField<T> entityField = fieldsMap[column];
                            string rawValue = cell.InnerText;

                            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
                            {
                                rawValue = sharedStrings[int.Parse(rawValue)];
                            }
                            else if (cell.CellFormula != null)
                            {
                                if (cell.CellValue == null)
                                {
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

                            if (columnNumber == lastColumn)
                            {
                                break;
                            }

                            cellIndex++;
                        }

                        if (!entityHasErrors)
                        {
                            parsedEntities.Add(new XlsxParsedRow<T>(rowNumber, currentSheetName, entity));
                        }

                        if (rowNumber == lastRow)
                        {
                            break;
                        }

                        rowIndex++;
                    }

                    if (sheetIndex == lastSheet)
                    {
                        break;
                    }

                    sheetIndex++;
                }
            }

            return new ProcessingResult<IEnumerable<IXlsxParsedRow<T>>>(parsedEntities, errors);
        }
    }
}
