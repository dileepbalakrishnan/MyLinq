using System;
using System.Collections.Generic;

namespace MyLinqImplementation
{
    public static partial class Enumerable
    {
        public static TSource FirstOrDefault<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
            {
                throw  new ArgumentNullException(nameof(source));
            }
            using (var enumerator = source.GetEnumerator())
            {
                return enumerator.MoveNext() ? enumerator.Current : default(TSource);
            }
        }

        public static TSource FirstOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            foreach (TSource item in source)
            {
                if (predicate(item))
                {
                    return item;
                }
            }
            return default(TSource);
        }
    }
}