using Plastiquewind.Base.Abstractions.Errors;
using Plastiquewind.Parsers.Abstractions.Errors;
using PommaLabs.Thrower;

namespace Plastiquewind.Parsers.Implementations.Errors
{
    public class XlsxRowErrorWrapper : IXlsxRowError
    {
        public XlsxRowErrorWrapper(int row, string sheetName, IError innerError)
        {
            Raise.ArgumentNullException.IfIsNull(sheetName, nameof(sheetName));
            Raise.ArgumentNullException.IfIsNull(innerError, nameof(innerError));

            Row = row;
            SheetName = sheetName;
            InnerError = innerError;
        }

        public virtual int Row { get; }

        public virtual string SheetName { get; }

        public virtual IError InnerError { get; }

        public string Message => InnerError.Message;
    }
}
