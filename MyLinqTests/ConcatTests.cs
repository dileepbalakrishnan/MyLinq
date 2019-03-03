using System;
using System.Collections.Generic;
using MyLinqImplementation;
using MyLinqTests.Helpers;
using NUnit.Framework;

namespace MyLinqTests
{
    [TestFixture]
    public class ConcatTests
    {
        [Test]
        public void SimpleConcat()
        {
            Enumerable.Range(1, 3).Concat(Enumerable.Range(4, 2)).AssertSequenceEqual(1, 2, 3, 4, 5);
        }

        [Test]
        public void NullFirstThrowsNullArgumentException()
        {
            IEnumerable<char> first = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.Throws(typeof(ArgumentNullException), () => first.Concat("Hello".ToCharArray()));
        }

        [Test]
        public void NullSecondThrowsNullArgumentException()
        {
            IEnumerable<char> second = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.Throws(typeof(ArgumentNullException), () => "Hello".ToCharArray().Concat(second));
        }

        [Test]
        public void FirstSequenceIsNotAccessedBeforeFirstUse()
        {
            var first = new ThrowingEnumerable();
            var query = first.Concat(new[] {1, 2, 3});
            using (var iterator = query.GetEnumerator())
            {
                Assert.Throws(typeof(InvalidOperationException), () => iterator.MoveNext());
            }
        }

        [Test]
        public void SecondSequenceIsNotAccessedBeforeFirstUse()
        {
            var second = new ThrowingEnumerable();
            var query = new[] {1, 2}.Concat(second);
            using (var iterator = query.GetEnumerator())
            {
                Assert.IsTrue(iterator.MoveNext());
                Assert.IsTrue(iterator.MoveNext());
                Assert.Throws(typeof(InvalidOperationException), () => iterator.MoveNext());
            }
        }
    }
}