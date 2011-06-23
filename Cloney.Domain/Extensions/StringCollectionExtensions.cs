using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace Cloney.Domain.Extensions
{
    public static class StringCollectionExtensions
    {
        public static IEnumerable<string> AsEnumerable(this StringCollection stringCollection)
        {
            return stringCollection == null ? null : stringCollection.Cast<string>().ToList();
        }

        public static bool IsNullOrEmpty(this StringCollection collection)
        {
            return collection == null || collection.Count == 0;
        }
    }
}
