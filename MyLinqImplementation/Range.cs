using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace MyLinqImplementation
{
    public static partial class Enumerable
    {
        public static IEnumerable<int> Range(int start, int count)
        {
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }
            if (start + (long) count - 1L > int.MaxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }
            return RangeImpl(start, count);
        }

        private static IEnumerable<int> RangeImpl(int start, int count)
        {
            for (var i = 0; i < count; i++)
            {
                yield return start + i;
            }
        }
    }
}