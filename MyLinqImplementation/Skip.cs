using System;
using System.Collections.Generic;

namespace MyLinqImplementation
{
    public partial class Enumerable
    {
        public static IEnumerable<TSource> Skip<TSource>(this IEnumerable<TSource> source, int count)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (count <= 0)
            {
                return source;
            }
            return SkipImpl(source, count);
        }

        private static IEnumerable<TSource> SkipImpl<TSource>(IEnumerable<TSource> source, int count)
        {
            using (var enumerator = source.GetEnumerator())
            {
                for (var i = 0; i < count; i++)
                {
                    if (!enumerator.MoveNext())
                    {
                        yield break;
                    }
                }
                while (enumerator.MoveNext())
                {
                    yield return enumerator.Current;    
                }
            }
        }
    }
}