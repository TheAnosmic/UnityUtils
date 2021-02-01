using System.Collections.Generic;

namespace Utils
{
    public static class StringUtils
    {
        public static string Join<T>(this IEnumerable<T> collection, string separator = ", ")
        {
            if (collection == null) return "";
            return string.Join(separator, collection);
        }
    }
}