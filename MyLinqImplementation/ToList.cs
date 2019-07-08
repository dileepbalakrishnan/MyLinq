using System;
using System.Collections.Generic;

namespace MyLinqImplementation
{
    public static partial class Enumerable
    {
        public static List<TSource> ToList<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            return new List<TSource>(source);
        }
    }
}