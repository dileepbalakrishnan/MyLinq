using System;
using System.Collections.Generic;

namespace MyLinqImplementation
{
    public static partial class Enumerable
    {
        public static IEnumerable<TSource> Except<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
        {
            if (first == null)
            {
                throw new ArgumentNullException(nameof(first));
            }
            if (second == null)
            {
                throw new ArgumentNullException(nameof(second));
            }
            return ExceptImpl(first, second, EqualityComparer<TSource>.Default);
        }

        public static IEnumerable<TSource> Except<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
        {
            if (first == null)
            {
                throw new ArgumentNullException(nameof(first));
            }
            if (second == null)
            {
                throw new ArgumentNullException(nameof(second));
            }
            return ExceptImpl(first, second, comparer ?? EqualityComparer<TSource>.Default);
        }
        private static IEnumerable<TSource> ExceptImpl<TSource>(IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
        {
            var itemsToBeSkipped = new HashSet<TSource>(second, comparer);
            foreach (var item in first)
            {
                if (itemsToBeSkipped.Add(item))
                {
                    yield return item;
                }
            }
        }
    }
}