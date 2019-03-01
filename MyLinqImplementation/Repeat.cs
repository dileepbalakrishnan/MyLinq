using System;
using System.Collections.Generic;

namespace MyLinqImplementation
{
    public static partial class Enumerable
    {
        public static IEnumerable<TResult> Repeat<TResult>(TResult element, int count)
        {
            if (count < 0)
                throw new ArgumentException("count");
            return RepeatImpl(element, count);
        }

        private static IEnumerable<TResult> RepeatImpl<TResult>(TResult element, int count)
        {
            for (var i = 0; i < count; i++)
                yield return element;
        }
    }
}