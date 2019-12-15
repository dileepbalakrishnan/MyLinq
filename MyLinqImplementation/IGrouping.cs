using System.Collections.Generic;

namespace MyLinqImplementation
{
    namespace Edulinq
    {
#if DOTNET35_ONLY
    public interface IGrouping<TKey, TElement> : IEnumerable<TElement>
#else
        public interface IGrouping<out TKey, out TElement> : IEnumerable<TElement>
#endif
        {
            TKey Key { get; }
        }
    }
}