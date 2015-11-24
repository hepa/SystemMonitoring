using System;

namespace SM.Core.Extensions
{
    public static class StringExtension
    {
        public static bool Contains(this string value, string toCheck, StringComparison sc) => value.IndexOf(toCheck, sc) >= 0;
    }
}