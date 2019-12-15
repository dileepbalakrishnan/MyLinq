using System;
using System.Collections.Generic;
using MyLinqImplementation.Edulinq;

namespace MyLinqImplementation
{
    public static partial class Enumerable
    {
        public static IEnumerable<IGrouping<TKey, TElement>> GroupBy<TSource, TKey, TElement>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            Func<TSource, TElement> elementSelector,
            IEqualityComparer<TKey> comparer)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector));
            if (elementSelector == null)
                throw new ArgumentNullException(nameof(elementSelector));
            return GroupByImpl(source, keySelector, elementSelector, comparer ?? EqualityComparer<TKey>.Default);
        }

        public static IEnumerable<IGrouping<TKey, TElement>> GroupByImpl<TSource, TKey, TElement>(
            IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            Func<TSource, TElement> elementSelector,
            IEqualityComparer<TKey> comparer)
        {
            var lookup = source.ToLookup(keySelector, elementSelector, comparer);
            foreach (var result in lookup)
                yield return result;
        }

        public static IEnumerable<TResult> GroupBy<TSource, TKey, TElement, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            Func<TSource, TElement> elementSelector,
            Func<TKey, IEnumerable<TElement>, TResult> resultSelector,
            IEqualityComparer<TKey> comparer)
        {
            if (resultSelector == null)
                throw new ArgumentNullException(nameof(resultSelector));
            return GroupBy(source, keySelector, elementSelector, comparer)
                .Select(group => resultSelector(group.Key, group));
        }
    }
}