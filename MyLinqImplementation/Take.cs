using System;
using System.Collections.Generic;

namespace MyLinqImplementation
{
    public partial class Enumerable
    {
        public static IEnumerable<TSource> Take<TSource>(this IEnumerable<TSource> source, int count)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            return TakeImpl(source, count);
        }

        private static IEnumerable<TSource> TakeImpl<TSource>(IEnumerable<TSource> source, int count)
        {
            using (var enumerator = source.GetEnumerator())
            {
                for (var i = 0; i < count && enumerator.MoveNext(); i++)
                {
                    yield return enumerator.Current;    
                }
            }
        }
    }
}