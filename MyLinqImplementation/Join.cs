using System;
using System.Collections.Generic;

namespace MyLinqImplementation
{
    public static partial class Enumerable
    {
        public static IEnumerable<TResult> Join<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer,
            IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector,
            Func<TOuter, TInner, TResult> resultSelector)
        {
            if (outer == null)
                throw new ArgumentNullException(nameof(outer));
            if (inner == null)
                throw new ArgumentNullException(nameof(inner));
            if (outerKeySelector == null)
                throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null)
                throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null)
                throw new ArgumentNullException(nameof(resultSelector));
            return JoinImpl(outer, inner, outerKeySelector, innerKeySelector, resultSelector,
                EqualityComparer<TKey>.Default);
        }

        public static IEnumerable<TResult> Join<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer,
            IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector,
            Func<TOuter, TInner, TResult> resultSelector, IEqualityComparer<TKey> comparer)
        {
            if (outer == null)
                throw new ArgumentNullException(nameof(outer));
            if (inner == null)
                throw new ArgumentNullException(nameof(inner));
            if (outerKeySelector == null)
                throw new ArgumentNullException(nameof(outerKeySelector));
            if (innerKeySelector == null)
                throw new ArgumentNullException(nameof(innerKeySelector));
            if (resultSelector == null)
                throw new ArgumentNullException(nameof(resultSelector));
            return JoinImpl(outer, inner, outerKeySelector, innerKeySelector, resultSelector,
                comparer ?? EqualityComparer<TKey>.Default);
        }

        private static IEnumerable<TResult> JoinImpl<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer,
            IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector,
            Func<TOuter, TInner, TResult> resultSelector, IEqualityComparer<TKey> comparer)
        {
            var lookup = inner.ToLookupNoNullKeys(innerKeySelector, comparer);
            foreach (var outerItem in outer)
            {
                var outerKey = outerKeySelector(outerItem);
                foreach (var innerItem in lookup[outerKey])
                    yield return resultSelector(outerItem, innerItem);
            }
        }
    }
}