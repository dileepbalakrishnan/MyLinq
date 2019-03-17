using System;
using System.Collections.Generic;
using MyLinqImplementation;
using MyLinqTests.Helpers;
using NUnit.Framework;

namespace MyLinqTests
{
    [TestFixture]
    public class UnionTests
    {
        [Test]
        public void NullFirstWithoutComparer()
        {
            IEnumerable<int> first = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.Throws(typeof(ArgumentNullException), () => first.Union(new[] {1, 2, 3}));
        }

        [Test]
        public void NullSecondWithoutComparer()
        {
            Assert.Throws(typeof(ArgumentNullException), () => new[] {1, 2, 3}.Union(null));
        }

        [Test]
        public void NullFirstWithComparer()
        {
            IEnumerable<string> first = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.Throws(typeof(ArgumentNullException),
                () => first.Union(new[] {"One"}, StringComparer.OrdinalIgnoreCase));
        }

        [Test]
        public void NullSecondWithComparer()
        {
            Assert.Throws(typeof(ArgumentNullException),
                () => new[] {1, 2, 3}.Union(null, EqualityComparer<int>.Default));
        }

        [Test]
        public void UnionWithoutComparer()
        {
            new[] {1, 2, 3}.Union(new[] {3, 4}).AssertSequenceEqual(1, 2, 3, 4);
        }

        [Test]
        public void UnionWithNullComparer()
        {
            new[] {"a", "b"}.Union(new[] {"A", "B"}).AssertSequenceEqual("a", "b", "A", "B");
        }

        [Test]
        public void UnionWithCaseInsensitiveComparer()
        {
            new[] {"a", "b"}.Union(new[] {"A", "B"}, StringComparer.OrdinalIgnoreCase)
                .AssertSequenceEqual("a", "b");
        }

        [Test]
        public void UnionWithEmptyFirstSequence()
        {
            new string[] { }.Union(new[] {"A", "B"}).AssertSequenceEqual("A", "B");
        }

        [Test]
        public void UnionWithEmptySecondSequence()
        {
            new[] {"A", "B"}.Union(new string[] { }).AssertSequenceEqual("A", "B");
        }

        [Test]
        public void UnionWithTwoEmptySequences()
        {
            new string[] { }.Union(new string[] { }).AssertSequenceEqual();
        }

        [Test]
        public void FirstSequenceIsNotUsedUntilQueryIsIterated()
        {
            var query = new ThrowingEnumerable().Union(new[] {1, 2});
            using (var enumerator = query.GetEnumerator()) // No exception expected here
            {
                Assert.Throws(typeof(InvalidOperationException), () => enumerator.MoveNext());
            }
        }

        [Test]
        public void SecondSequenceIsNotUsedUntilFirstIsExhausted()
        {
            var query = new[] {1, 2}.Union(new ThrowingEnumerable());
            using (var enumerator = query.GetEnumerator()) // No exception expected here
            {
                Assert.IsTrue(enumerator.MoveNext());// No exception expected here - 1
                Assert.IsTrue(enumerator.MoveNext());// No exception expected here - 2
                Assert.Throws(typeof(InvalidOperationException), () => enumerator.MoveNext());
            }
        }
    }
}