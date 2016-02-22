using System.Collections;

namespace SM.Core.Extensions
{
    public static class CollectionExtensions
    {
        public static bool IsEmpty(this ICollection collection)
        {
            return collection.Count == 0;
        }
    }
}