using System.Linq;
using System.Text.RegularExpressions;

namespace Plastiquewind.Parsers.Helpers
{
    public static class XlsxCellAddressParser
    {
        public static string GetColumn(string cellAddress)
        {
            return new string(cellAddress.Where(c => c < '0' || c > '9').ToArray());
        }

        public static int GetRow(string cellAddress)
        {
            return int.Parse(Regex.Replace(cellAddress, "[A-Za-z]", string.Empty));
        }
    }
}
