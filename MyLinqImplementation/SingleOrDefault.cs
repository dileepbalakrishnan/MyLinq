﻿using System;
using System.Collections.Generic;

namespace MyLinqImplementation
{
    public static partial class Enumerable
    {
        public static TSource SingleOrDefault<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
            {
                throw  new ArgumentNullException(nameof(source));
            }
            using (var enumerator = source.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                {
                    return default(TSource);
                }
                TSource item = enumerator.Current;
                if (enumerator.MoveNext())
                {
                    throw new InvalidOperationException("Sequence contains multiple elements.");
                }
                return item;
            }
        }

        public static TSource SingleOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            TSource match = default(TSource);
            bool foundMatch = false;
            foreach (TSource item in source)
            {
                if (predicate(item))
                {
                    if (foundMatch)
                    {
                        throw new InvalidOperationException("Sequence contains multiple matching elements.");
                    }
                    foundMatch = true;
                    match = item;
                }
            }
            return match;
        }
    }
}