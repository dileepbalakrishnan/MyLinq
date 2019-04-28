using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MyLinqTests;

namespace MyLinqImplementation
{
    internal class Lookup<TKey, TElement> : ILookup<TKey, TElement>
    {
        private readonly NullKeyFriendlyDictionary<TKey, IList<TElement>> _map;
        private readonly IList<TKey> _keys;

        internal Lookup(IEqualityComparer<TKey> comparer)
        {
            _map = new NullKeyFriendlyDictionary<TKey, IList<TElement>>(comparer);
            _keys = new List<TKey>();
        }
        public IEnumerator<IGrouping<TKey, TElement>> GetEnumerator()
        {
            return _keys.Select(k => new Grouping<TKey, TElement>(k, _map[k])).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool Contains(TKey key)
        {
            return _map.ContainsKey(key);
        }

        public int Count => _map.Count;

        public IEnumerable<TElement> this[TKey key]
        {
            get
            {
                if (_map.ContainsKey(key))
                {
                    return _map[key].Select(x => x);
                }
                return Enumerable.Empty<TElement>();
            }
        }

        internal void Add(TKey key, TElement element)
        {
            if (_map.ContainsKey(key))
            {
                _map[key].Add(element);
            }
            else
            {
                _map[key] = new List<TElement>{element};
                _keys.Add(key);
            }
        }
    }
}