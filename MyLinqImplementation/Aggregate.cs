using System;
using System.Collections.Generic;

namespace MyLinqImplementation
{
    public partial class Enumerable
    {
        public static TSource Aggregate<TSource>(this IEnumerable<TSource> source, Func<TSource, TSource, TSource> func)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (func == null)
            {
                throw new ArgumentNullException(nameof(func));
            }
            using (var enumerator = source.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                {
                    throw new InvalidOperationException("Source sequence is empty.");
                }
                TSource accumulator = enumerator.Current;
                while (enumerator.MoveNext())
                {
                    accumulator = func(accumulator, enumerator.Current);
                }
                return accumulator;
            }
        }

        public static TAccumulate Aggregate<TSource, TAccumulate>(this IEnumerable<TSource> source, TAccumulate seed,
            Func<TAccumulate, TSource, TAccumulate> func)
        {
            return Aggregate(source, seed, func, x => x);
        }

        public static TResult Aggregate<TSource, TAccumulate, TResult>(this IEnumerable<TSource> source,
            TAccumulate seed,
            Func<TAccumulate, TSource, TAccumulate> func, Func<TAccumulate, TResult> resultSelector)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (func == null)
            {
                throw new ArgumentNullException(nameof(func));
            }

            if (resultSelector == null)
            {
                throw new ArgumentNullException(nameof(resultSelector));
            }

            TAccumulate accumulator = seed;

            foreach (TSource item in source)
            {
                accumulator = func(accumulator, item);
            }
            return resultSelector(accumulator);
        }
    }
}