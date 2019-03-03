using System;
using System.Collections.Generic;

namespace MyLinqImplementation
{
    public partial class Enumerable
    {
        public static IEnumerable<TSource> Concat<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
        {
            if (first == null)
                throw new ArgumentNullException(nameof(first));

            if (second == null)
                throw new ArgumentNullException(nameof(second));

            return ConcatImpl(first, second);
        }

        private static IEnumerable<TSource> ConcatImpl<TSource>(IEnumerable<TSource> first, IEnumerable<TSource> second)
        {
            foreach (var item in first)
            {
                yield return item;
            }
            first = null;
            foreach (var item in second)
            {
                yield return item;
            }
        }
    }
}