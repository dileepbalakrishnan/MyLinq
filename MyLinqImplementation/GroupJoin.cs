using System;
using System.Collections.Generic;

namespace MyLinqImplementation
{
    public partial class Enumerable
    {
        public static IEnumerable<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer,
            IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector,
            Func<TOuter, IEnumerable<TInner>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
        {
            if (outer == null)
            {
                throw new ArgumentException(nameof(outer));
            }

            if (inner == null)
            {
                throw new ArgumentException(nameof(inner));
            }

            if (outerKeySelector == null)
            {
                throw new ArgumentException(nameof(outerKeySelector));
            }

            if (innerKeySelector == null)
            {
                throw new ArgumentException(nameof(innerKeySelector));
            }

            if (resultSelector == null)
            {
                throw new ArgumentException(nameof(resultSelector));
            }

            return GroupJoinImpl(outer, inner, outerKeySelector, innerKeySelector, resultSelector,
                comparer ?? EqualityComparer<TKey>.Default);
        }

        private static IEnumerable<TResult> GroupJoinImpl<TOuter, TInner, TKey, TResult>(IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, IEnumerable<TInner>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
        {
            var lookup = inner.ToLookupNoNullKeys(innerKeySelector, comparer);
            foreach (var outerElement in outer)
            {
                var key = outerKeySelector(outerElement);
                yield return resultSelector(outerElement, lookup[key]);
            }
        }
    }
}