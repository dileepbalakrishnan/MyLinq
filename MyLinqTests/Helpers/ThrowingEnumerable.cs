using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace MyLinqTests.Helpers
{
    internal class ThrowingEnumerable : IEnumerable<int>
    {
        public IEnumerator<int> GetEnumerator()
        {
            return new ThrowingEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        internal static void AssertDeferred<T>(
            Func<IEnumerable<int>, IEnumerable<T>> deferredFunction)
        {
            var source = new ThrowingEnumerable();
            IEnumerable<T> result = deferredFunction(source);
            using (IEnumerator<T> iterator = result.GetEnumerator())
            {
                Assert.Throws<InvalidOperationException>(() => iterator.MoveNext());
            }
        }
    }
}