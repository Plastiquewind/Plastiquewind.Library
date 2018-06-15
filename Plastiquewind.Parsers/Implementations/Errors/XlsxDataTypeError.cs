using Plastiquewind.Parsers.Abstractions.Errors;
using Plastiquewind.Parsers.Helpers;
using Plastiquewind.Parsers.Resources;
using PommaLabs.Thrower;

namespace Plastiquewind.Parsers.Implementations.Errors
{
    public class XlsxDataTypeError : IXlsxRowError
    {
        public XlsxDataTypeError(string field, string cellAddress, string cellSheetName)
        {
            Raise.ArgumentNullException.IfIsNull(field, nameof(field));
            Raise.ArgumentNullException.IfIsNull(cellAddress, nameof(cellAddress));
            Raise.ArgumentNullException.IfIsNull(cellSheetName, nameof(cellSheetName));

            Field = field;
            CellAddress = cellAddress;
            SheetName = cellSheetName;
        }

        public virtual string CellAddress { get; }
        public virtual string Column => XlsxCellAddressParser.GetColumn(CellAddress);
        public virtual int Row => XlsxCellAddressParser.GetRow(CellAddress);
        public virtual string SheetName { get; }
        public virtual string Field { get; }
        public virtual string Message => string.Format(ErrorMessages.XlsxDataTypeError, CellAddress, SheetName, Field);
    }
}
