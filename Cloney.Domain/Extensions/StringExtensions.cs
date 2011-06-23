using System;

namespace Cloney.Domain.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string @string, bool trim = true)
        {
            if (@string == null)
                return true;

            @string = trim ? @string.Trim() : @string;
            return String.IsNullOrEmpty(@string);
        }
    }
}
