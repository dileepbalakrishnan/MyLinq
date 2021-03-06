﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace MyLinqTests.Helpers
{
    public class NonEnumerableList<T> : IList<T>
    {
        private readonly List<T> _list = new List<T>();

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotSupportedException();
        }

        public NonEnumerableList(IEnumerable<T> list)
        {
            _list = new List<T>(list);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            _list.Add(item);
        }

        public void Clear()
        {
            _list.Clear();
        }

        public bool Contains(T item)
        {
            return _list.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            return _list.Remove(item);
        }

        public int Count
        {
            get { return _list.Count; }
        }

        public bool IsReadOnly
        {
            get { return ((ICollection<T>) _list).IsReadOnly; }
        }

        public int IndexOf(T item)
        {
            return _list.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            _list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _list.RemoveAt(index);
        }

        public T this[int index]
        {
            get { return _list[index]; }
            set { _list[index] = value; }
        }
    }
}