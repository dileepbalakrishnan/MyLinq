using System;
using System.Collections.Generic;

namespace MyLinqImplementation
{
    public static partial class Enumerable
    {
        public static IEnumerable<TSource> Intersect<TSource>(this IEnumerable<TSource> first,
            IEnumerable<TSource> second)
        {
            if (first == null)
            {
                throw new ArgumentNullException(nameof(first));
            }
            if (second == null)
            {
                throw new ArgumentNullException(nameof(second));
            }
            return IntersectImpl(first, second, EqualityComparer<TSource>.Default);
        }

        public static IEnumerable<TSource> Intersect<TSource>(this IEnumerable<TSource> first,
            IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
        {
            if (first == null)
            {
                throw new ArgumentNullException(nameof(first));
            }
            if (second == null)
            {
                throw new ArgumentNullException(nameof(second));
            }
            return IntersectImpl(first, second, comparer ?? EqualityComparer<TSource>.Default);
        }

        private static IEnumerable<TSource> IntersectImpl<TSource>(IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
        {
            HashSet<TSource> itemsInSecond = new HashSet<TSource>(second, comparer);
            foreach (TSource itemInFirst in first)
            {
                if (itemsInSecond.Remove(itemInFirst))
                {
                    yield return itemInFirst;
                }
            }
        }
    }
}