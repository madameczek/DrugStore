using System;
using System.Collections.Generic;
using System.Text;

namespace ActiveRecord.DataModels
{
    public static class PeselChecker
    {
        private static readonly int[] factor = { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 };
                 
        public static bool Verify(string pesel)
        {
            if (pesel.Length != 11) { return false; }

            int sum = 0;
            for (int digit = 0; digit < 10; digit++)
            {
                int i = (int)Char.GetNumericValue(pesel[digit]); int v = factor[digit];
                sum += (int)Char.GetNumericValue(pesel[digit]) * factor[digit];
            }
            sum %= 10;
            sum = 10 - sum;
            sum %= 10;
            if (sum == (int)Char.GetNumericValue(pesel[10]))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
