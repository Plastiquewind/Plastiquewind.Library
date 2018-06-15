using Plastiquewind.Parsers.Abstractions;

namespace Plastiquewind.Parsers.Implementations
{
    public class XlsxParsedRow<T> : IXlsxParsedRow<T>
    {
        public XlsxParsedRow(int number, string sheetName, T value)
        {
            Number = number;
            SheetName = sheetName;
            Value = value;
        }

        public virtual int Number { get; }
        public virtual string SheetName { get; }
        public virtual T Value { get; }
    }
}
