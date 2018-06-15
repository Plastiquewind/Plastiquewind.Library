using System.Linq;

namespace Plastiquewind.Parsers.Helpers
{
    public static class XlsxColumnAddressConverter
    {
        // From https://stackoverflow.com/a/31035990/6265012
        public static int ToInt(string columnAddress)
        {
            int columnNumber = -1;
            int multiplier = 1;

            //working from the end of the letters take the ASCII code less 64 (so A = 1, B = 2...etc)
            //then multiply that number by our multiplier (which starts at 1)
            //multiply our multiplier by 26 as there are 26 letters
            foreach (char c in columnAddress.ToCharArray().Reverse())
            {
                columnNumber += multiplier * (c - 64);

                multiplier = multiplier * 26;
            }

            //the result is zero based so return columnnumber + 1 for a 1 based answer
            //this will match Excel's COLUMN function
            return columnNumber + 1;
        }
    }
}
