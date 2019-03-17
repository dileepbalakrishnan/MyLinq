using System;
using System.Collections.Generic;

namespace MyLinqImplementation
{
    public static partial class Enumerable
    {
        public static IEnumerable<TSource> Union<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
        {
            if (first == null)
            {
                throw new ArgumentNullException(nameof(first));
            }
            if (second == null)
            {
                throw new ArgumentNullException(nameof(second));
            }
            return UnionImpl(first, second, EqualityComparer<TSource>.Default);
        }

        public static IEnumerable<TSource> Union<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
        {
            if (first == null)
            {
                throw new ArgumentNullException(nameof(first));
            }
            if (second == null)
            {
                throw new ArgumentNullException(nameof(second));
            }
            return UnionImpl(first, second, comparer);
        }


        private static IEnumerable<TSource> UnionImpl<TSource>(IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
        {
            var yieldedItems = new HashSet<TSource>(comparer);

            foreach (TSource item in first)
            {
                if (yieldedItems.Add(item))
                {
                    yield return item;
                }
            }
            foreach (TSource item in second)
            {
                if (yieldedItems.Add(item))
                {
                    yield return item;
                }
            }
        }
    }
}