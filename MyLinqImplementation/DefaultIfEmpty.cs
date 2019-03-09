using System;
using System.Collections.Generic;

namespace MyLinqImplementation
{
    public partial class Enumerable
    {
        public static IEnumerable<TSource> DefaultIfEmpty<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            return DefaultIfEmptyImpl(source, default(TSource));
        }

        public static IEnumerable<TSource> DefaultIfEmpty<TSource>(this IEnumerable<TSource> source, TSource defaultValue)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            return DefaultIfEmptyImpl(source, defaultValue);
        }

        private static IEnumerable<TSource> DefaultIfEmptyImpl<TSource>(IEnumerable<TSource> source, TSource defaultValue)
        {
            using (var enumerator = source.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                {
                    yield return defaultValue;
                }
                else
                {
                    do
                    {
                        yield return enumerator.Current;
                    } while (enumerator.MoveNext());
                }
            }
        }
    }
}