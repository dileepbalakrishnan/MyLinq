using System.Collections.Generic;

namespace MyLinqImplementation
{
    public static partial class Enumerable
    {
        public static IEnumerable<TResult> Empty<TResult>()
        {
            return EmptyEnumerable<TResult>.Instance;
        }

        internal class EmptyEnumerable<TSource>
        {
            public static readonly TSource[] Instance = new TSource[0];
        }
    }
}
