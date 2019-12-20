using System;
using System.Collections.Generic;

namespace MyLinqImplementation
{
    public partial class Enumerable
    {
        public static TSource[] ToArray<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            return ToArrayImpl(source);
        }

        private static TSource[] ToArrayImpl<TSource>(IEnumerable<TSource> source)
        {
            if (source is ICollection<TSource> collection)
            {
                var array = new TSource[collection.Count];
                collection.CopyTo(array, 0);
                return array;
            }
            return new List<TSource>(source).ToArray();
        }
    }
}
