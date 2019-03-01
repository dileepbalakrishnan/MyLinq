using System;
using System.Collections;
using System.Collections.Generic;

namespace MyLinqImplementation
{
    public static partial class Enumerable
    {
        public static long LongCount<TSource>(this IEnumerable<TSource> source, Func<TSource,bool> predicate)
        {
            if (source == null)
            {
                throw new ArgumentException("source");
            }

            if (predicate == null)
            {
                throw new ArgumentException("predicate");
            }
            checked
            {
                long count = 0;
                foreach (var item in source)
                {
                    if (predicate(item))
                    {
                        count++;
                    }
                }
                return count;
            }
        }

        public static long LongCount<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
            {
                throw new ArgumentException("source");
            }
              
            if (source is ICollection<TSource> genericCollection)
            {
                return genericCollection.Count;
            }

            if (source is ICollection nonGenericCollection)
            {
                return nonGenericCollection.Count;
            }

            checked
            {
                long count = 0;
                foreach (var item in source)
                {
                    count++;
                }
                return count;
            }
        }
    }
}