using System.Linq;

using System;

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
		
		// From https://stackoverflow.com/questions/181596/how-to-convert-a-column-number-eg-127-into-an-excel-column-eg-aa
        public static string ToString(int columnAddress)
        {
            int dividend = columnAddress;
            string columnName = string.Empty;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = ((dividend - modulo) / 26);
            }

            return columnName;
        }
    }
}
