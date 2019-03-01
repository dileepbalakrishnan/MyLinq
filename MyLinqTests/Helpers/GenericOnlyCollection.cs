using System.Collections;
using System.Collections.Generic;

namespace MyLinqTests.Helpers
{
    public class GenericOnlyCollection<T> : ICollection<T>
    {
        private readonly List<T> _backingList;

        public GenericOnlyCollection(IEnumerable<T> items)
        {
            _backingList = new List<T>(items);
        }

        public void Add(T item)
        {
            _backingList.Add(item);
        }

        public void Clear()
        {
            _backingList.Clear();
        }

        public bool Contains(T item)
        {
            return _backingList.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _backingList.CopyTo(array, arrayIndex);
        }

        public int Count => _backingList.Count;

        public bool IsReadOnly => ((ICollection<T>) _backingList).IsReadOnly;

        public bool Remove(T item)
        {
            return _backingList.Remove(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _backingList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
