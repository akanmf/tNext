using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tNext.Common.Core.Helpers
{
    public class StringHelper
    {
        public static string CleanString(string input)
        {
            return input
                .Replace("İ", "I")
                .Replace("ı", "i")
                .Replace("Ğ", "G")
                .Replace("ğ", "g")
                .Replace("Ö", "O")
                .Replace("ö", "o")
                .Replace("Ü", "U")
                .Replace("ü", "u")
                .Replace("Ş", "S")
                .Replace("ş", "s")
                .Replace("Ç", "C")
                .Replace("ç", "c");

        }


        public static string PrepareStringForSearch(string input)
        {
            return input
                .Replace("'", " ")
                .Replace("-", " ")
                .Replace("/", " ")
                .Replace("\\", " ")
                .Replace("*", " ")
                .ToLower();

        }
    }
}
