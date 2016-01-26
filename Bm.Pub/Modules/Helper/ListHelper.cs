using System.Collections.Generic;
using System.Linq;

namespace Bm.Modules.Helper
{
    public static class ListHelper
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> array)
        {
            return array == null || !array.Any();
        }

        public static bool IsNullOrEmpty<T>(this ICollection<T> array)
        {
            return array == null || !array.Any();
        }

        public static bool IsNullOrEmpty<T>(this IList<T> array)
        {
            return array == null || !array.Any();
        }
        
        public static bool IsNullOrEmpty<T>(this List<T> array)
        {
            return array == null || !array.Any();
        }
    }
}
