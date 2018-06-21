using Plastiquewind.Base.Abstractions;
using DocumentFormat.OpenXml.Packaging;

namespace Plastiquewind.Parsers.Abstractions
{
    public interface IXlsxParserConfigFactory<T>
    {
        IProcessingResult<IXlsxParserConfig<T>> Create(SpreadsheetDocument spreadsheetDocument);
    }
}
