using System.Collections.Generic;

namespace MyLinqImplementation
{
    internal sealed class NullKeyFriendlyDictionary<TKey, TValue>
    {
        private readonly Dictionary<TKey, TValue> _map;
        private bool _haveNullKey;
        private TValue _valueForNullKey;

        internal NullKeyFriendlyDictionary(IEqualityComparer<TKey> comparer)
        {
            _map = new Dictionary<TKey, TValue>(comparer);
        }

        internal bool TryGetValue(TKey key, out TValue value)
        {
            if (key == null)
            {
                value = _valueForNullKey;
                return _haveNullKey;
            }
            return _map.TryGetValue(key, out value);
        }

        internal TValue this[TKey key]
        {
            get
            {
                if (key == null)
                {
                    if (_haveNullKey)
                    {
                        return _valueForNullKey;
                    }
                    throw new KeyNotFoundException("No null key");
                }
                return _map[key];
            }
            set
            {
                if (key == null)
                {
                    _haveNullKey = true;
                    _valueForNullKey = value;
                }
                else
                {
                    _map[key] = value;
                }
            }
        }

        internal int Count => _map.Count + (_haveNullKey ? 1 : 0);

        internal bool ContainsKey(TKey key)
        {
            return key == null ? _haveNullKey : _map.ContainsKey(key);
        }
    }
}