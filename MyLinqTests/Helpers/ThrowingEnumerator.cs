using System;
using System.Collections;
using System.Collections.Generic;

namespace MyLinqTests.Helpers
{
    internal class ThrowingEnumerator : IEnumerator<int>
    {
        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            throw new InvalidOperationException();
        }

        public void Reset()
        {
            throw new InvalidOperationException();
        }

        public int Current { get; private set; }

        object IEnumerator.Current
        {
            get { return Current; }
        }
    }
}