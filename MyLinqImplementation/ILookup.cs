using System.Collections.Generic;
using MyLinqImplementation.Edulinq;

namespace MyLinqImplementation
{
    public interface ILookup<TKey, TElement> : IEnumerable<IGrouping<TKey, TElement>>
    {
        int Count { get; }
        IEnumerable<TElement> this[TKey key] { get; }
        bool Contains(TKey key);
    }
}