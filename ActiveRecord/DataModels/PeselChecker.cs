using System;
using System.Collections.Generic;
using System.Text;

namespace ActiveRecord.DataModels
{
    // Basic implementation
    public static class PeselChecker
    {
        private static readonly int[] factor = { 9, 7, 3, 1 };
                 
        public static bool Verify(string pesel)
        {
            if (pesel.Length != 11) { return false; }

            int sum = 0;
            for (int digit = 0; digit < 10; digit++)
            {
                sum += (int)Char.GetNumericValue(pesel[digit]) * factor[digit % 4];
            }
            sum %= 10;
            return sum == (int)Char.GetNumericValue(pesel[10]);
        }
    }
}
