using System;
using System.Collections.Generic;

namespace MyLinqImplementation
{
    public partial class Enumerable
    {
        public static IEnumerable<TSource> SkipWhile<TSource>(this IEnumerable<TSource> source,
            Func<TSource, int, bool> predicate)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return SkipWhileImpl(source, predicate);
        }

        private static IEnumerable<TSource> SkipWhileImpl<TSource>(IEnumerable<TSource> source,
            Func<TSource, int, bool> predicate)
        {
            using (var enumerator = source.GetEnumerator())
            {
                var index = 0;
                while (enumerator.MoveNext())
                {
                    if (predicate(enumerator.Current, index))
                    {
                        index++;
                        continue;
                    }
                    yield return enumerator.Current;
                    break;
                }
                while (enumerator.MoveNext())
                {
                    yield return enumerator.Current;
                }
            }
        }

        public static IEnumerable<TSource> SkipWhile<TSource>(this IEnumerable<TSource> source,
            Func<TSource, bool> predicate)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return SkipWhileImpl(source, predicate);
        }

        private static IEnumerable<TSource> SkipWhileImpl<TSource>(IEnumerable<TSource> source,
            Func<TSource, bool> predicate)
        {
            using (var enumerator = source.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    if (predicate(enumerator.Current))
                    {
                        continue;
                    }
                    yield return enumerator.Current;
                    break;
                }
                while (enumerator.MoveNext())
                {
                    yield return enumerator.Current;
                }
            }
        }
    }
}