using System;
using System.Collections.Generic;
using MyLinqImplementation;
using NUnit.Framework;

namespace MyLinqTests
{
    [TestFixture]
    public class SingleOrDefaultTests
    {
        [Test]
        public void NullSourceWithoutPredicate()
        {
            IEnumerable<int> source = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.Throws(typeof(ArgumentNullException), () => source.SingleOrDefault());
        }

        [Test]
        public void NullSourceWithPredicate()
        {
            IEnumerable<int> source = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.Throws(typeof(ArgumentNullException), () => source.SingleOrDefault(x => x > 1));
        }

        [Test]
        public void NullPredicate()
        {
            Assert.Throws(typeof(ArgumentNullException), () => new[] { 1 }.SingleOrDefault(null));
        }

        [Test]
        public void EmptySequenceWithoutPredicate()
        {
            Assert.AreEqual(0, new int[0].SingleOrDefault());
        }

        [Test]
        public void SingleElementSequenceWithoutPredicate()
        {
            Assert.AreEqual(1, new[] { 1 }.SingleOrDefault());
        }

        [Test]
        public void MultipleElementSequenceWithoutPredicate()
        {
            Assert.Throws<InvalidOperationException>(() => new[] { 1, 2, 3 }.SingleOrDefault());
        }

        [Test]
        public void EmptySequenceWithPredicate()
        {
            Assert.AreEqual(0, new int[0].SingleOrDefault(x => x > 2));
        }

        [Test]
        public void SingleElementSequenceWithMatchingPredicate()
        {
            Assert.AreEqual(3, new[] { 3 }.SingleOrDefault(x => x >= 3));
        }

        [Test]
        public void SingleElementSequenceWithNonMatchingPredicate()
        {
            Assert.AreEqual(0, new[] { 3 }.SingleOrDefault(x => x > 3));
        }

        [Test]
        public void MultipleElementSequenceWithNoPredicateMatches()
        {
            Assert.AreEqual(0, new[] {3, 4, 5}.SingleOrDefault(x => x > 5));
        }

        [Test]
        public void MultipleElementSequenceWithSinglePredicateMatch()
        {
            Assert.AreEqual(5, new[] { 3, 4, 5 }.SingleOrDefault(x => x > 4));
        }

        [Test]
        public void MultipleElementSequenceWithMultiplePredicateMatches()
        {
            Assert.Throws(typeof(InvalidOperationException), () => new[] { 3, 4, 5 }.SingleOrDefault(x => x > 3));
        }

        [Test]
        public void EarlyOutWithoutPredicate()
        {
            Assert.Throws(typeof(InvalidOperationException),
                () => new[] { 3, 4, 0 }.Select(x => 10 / x).SingleOrDefault());
        }

        [Test]
        public void EarlyOutWithPredicate()
        {
            Assert.Throws(typeof(InvalidOperationException),
                () => new[] { 3, 4, 0 }.Select(x => 12 / x).SingleOrDefault(x => x > 2));
        }
    }
}