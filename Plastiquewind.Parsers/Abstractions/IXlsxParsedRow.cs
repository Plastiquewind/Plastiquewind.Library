namespace Plastiquewind.Parsers.Abstractions
{
    public interface IXlsxParsedRow<T>
    {
        int Number { get; }

        string SheetName { get; }

        T Value { get; }
    }
}
