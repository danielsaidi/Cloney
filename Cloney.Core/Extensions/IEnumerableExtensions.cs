using System.Collections.Generic;
using System.Linq;

namespace Cloney.Core.Extensions
{
    public static class IEnumerableExtensions
    {
        public static bool Contains<T>(this IEnumerable<T> collection, T obj, bool handleNull)
        {
            if (handleNull && collection == null)
                return false;
            return collection.Contains(obj);
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection)
        {
            return collection == null || collection.Count() == 0;
        }
    }
}
