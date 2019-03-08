using System;
using System.Collections.Generic;

namespace MyLinqImplementation
{
    public static partial class Enumerable
    {
        public static TSource Last<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            // Optimization : method returns faster when sequence is a list.
            if (source is IList<TSource> list && list.Count != 0)
            {
                return list[list.Count - 1];
            }

            TSource last = default(TSource);
            bool foundMatch = false;
            foreach (TSource item in source)
            {
                last = item;
                foundMatch = true;
            }
            if (!foundMatch)
            {
                throw new InvalidOperationException("Sequence was empty");
            }
            return last;
        }

        public static TSource Last<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            TSource last = default(TSource);
            bool foundMatch = false;
            foreach (TSource item in source)
            {
                if (predicate(item))
                {
                    last = item;
                    foundMatch = true;
                }
            }
            if (!foundMatch)
            {
                throw new InvalidOperationException("No items matched the predicate");
            }
            return last;
        }
    }
}