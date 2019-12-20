using System;
using System.Collections.Generic;

namespace MyLinqImplementation
{
    public partial class Enumerable
    {
        public static IEnumerable<TSource> TakeWhile<TSource>(this IEnumerable<TSource> source, Func<TSource, int, bool> predicate)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return TakeWhileImpl(source, predicate);
        }

        private static IEnumerable<TSource> TakeWhileImpl<TSource>(IEnumerable<TSource> source, Func<TSource, int, bool> predicate)
        {
            using (var enumerator = source.GetEnumerator())
            {
                var index = 0;
                while (enumerator.MoveNext() && predicate(enumerator.Current, index))
                {
                    index++;
                    yield return enumerator.Current;
                }
            }
        }

        public static IEnumerable<TSource> TakeWhile<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return TakeWhileImpl(source, predicate);
        }

        private static IEnumerable<TSource> TakeWhileImpl<TSource>(IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            using (var enumerator = source.GetEnumerator())
            {
                while (enumerator.MoveNext() && predicate(enumerator.Current))
                {
                    yield return enumerator.Current;
                }
            }
        }

    }
}