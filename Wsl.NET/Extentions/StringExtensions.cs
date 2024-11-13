using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
    public static class StringExtensions
    {
        public static bool IsMissing(this string @string)
        {
            return string.IsNullOrEmpty(@string);
        }

        public static bool IsPresent(this string @string)
        {
            return !string.IsNullOrEmpty(@string);
        }
    }
}
