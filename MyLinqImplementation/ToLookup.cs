using System;
using System.Collections.Generic;
using System.Linq;

namespace MyLinqImplementation
{
    public static partial class Enumerable
    {
        public static ILookup<TKey, TSource> ToLookup<TKey, TSource>(this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector)
        {
            return ToLookup(source, keySelector, EqualityComparer<TKey>.Default);
        }

        public static ILookup<TKey, TElement> ToLookup<TSource, TKey, TElement>(this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector,
            IEqualityComparer<TKey> comparer)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector));

            if (elementSelector == null)
                throw new ArgumentNullException(nameof(elementSelector));

            var lookup = new Lookup<TKey, TElement>(comparer ?? EqualityComparer<TKey>.Default);
            foreach (var item in source)
            {
                var key = keySelector(item);
                var element = elementSelector(item);
                lookup.Add(key, element);
            }
            return lookup;
        }

        public static ILookup<TKey, TSource> ToLookup<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector));

            var lookup = new Lookup<TKey, TSource>(comparer ?? EqualityComparer<TKey>.Default);
            foreach (var item in source)
            {
                var key = keySelector(item);
                lookup.Add(key, item);
            }
            return lookup;
        }
        public static ILookup<TKey, TElement> ToLookup<TSource, TKey, TElement>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            Func<TSource, TElement> elementSelector)
        {
            return ToLookup(source, keySelector, elementSelector, EqualityComparer<TKey>.Default);
        }
    }
}