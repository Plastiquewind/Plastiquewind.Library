using Plastiquewind.Base.Abstractions.Errors;

namespace Plastiquewind.Parsers.Abstractions.Errors
{
    public interface IXlsxRowError : IError
    {
        int Row { get; }

        string SheetName { get; }
    }
}
