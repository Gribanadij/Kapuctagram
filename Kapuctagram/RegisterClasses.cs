using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kapuctagram
{
    public static class StringUtils
    {
        public static int GetLength(string input)
        {
            return input.Length;
        }

        public static int GetUppercaseCount(string input)
        {
            return input.Count(char.IsUpper);
        }

        public static int GetSpecialCharCount(string input)
        {
            return input.Count(c => !char.IsLetterOrDigit(c));
        }
    }
}
