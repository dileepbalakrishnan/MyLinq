using System;
using System.Collections.Generic;

namespace MyLinqImplementation
{
    public static partial class Enumerable
    {
        public static IEnumerable<TSource> Distinct<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            return DistinctImpl(source, EqualityComparer<TSource>.Default);
        }

        public static IEnumerable<TSource> Distinct<TSource>(this IEnumerable<TSource> source, IEqualityComparer<TSource> comparer)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            return DistinctImpl(source, comparer);
        }

        private static IEnumerable<TSource> DistinctImpl<TSource>(IEnumerable<TSource> source, IEqualityComparer<TSource> comparer)
        {
            var seenItems = new HashSet<TSource>(comparer);
            foreach (var item in source)
            {
                if (seenItems.Add(item))
                {
                    yield return item;
                }
            }
        }
    }
}