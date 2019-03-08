using System;
using System.Collections.Generic;
using MyLinqImplementation;
using NUnit.Framework;

namespace MyLinqTests
{
    [TestFixture]
    public class FirstTests
    {
        [Test]
        public void NullSourceWithoutPredicate()
        {
            IEnumerable<int> source = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.Throws(typeof(ArgumentNullException), () => source.First());
        }

        [Test]
        public void NullSourceWithPredicate()
        {
            IEnumerable<int> source = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.Throws(typeof(ArgumentNullException), () => source.First(x => x > 2));
        }

        [Test]
        public void NullPredicate()
        {
            Assert.Throws(typeof(ArgumentNullException), () => new[] {1}.First(null));
        }

        [Test]
        public void EmptySequenceWithoutPredicate()
        {
            Assert.Throws(typeof(InvalidOperationException), () => new int[0].First());
        }

        [Test]
        public void SingleElementSequenceWithoutPredicate()
        {
            Assert.AreEqual(1, new [] {1}.First());
        }

        [Test]
        public void MultipleElementSequenceWithoutPredicate()
        {
            Assert.AreEqual(1, new[] { 1, 2, 3 }.First());
        }

        [Test]
        public void EmptySequenceWithPredicate()
        {
            Assert.Throws(typeof(InvalidOperationException), () => new int[0].First(x => x > 2));
        }

        [Test]
        public void SingleElementSequenceWithMatchingPredicate()
        {
            Assert.AreEqual(3, new [] {3}.First(x => x > 2));
        }

        [Test]
        public void SingleElementSequenceWithNonMatchingPredicate()
        {
            Assert.Throws(typeof(InvalidOperationException), () => new[] {3}.First(x => x > 4));
        }

        [Test]
        public void MultipleElementSequenceWithNoPredicateMatches()
        {
            Assert.Throws(typeof(InvalidOperationException), () => new[] {3, 2, 1}.First(x => x > 4));
        }

        [Test]
        public void MultipleElementSequenceWithSinglePredicateMatch()
        {
            Assert.AreEqual(3, new[] {3, 2, 1}.First(x => x > 2));
        }

        [Test]
        public void MultipleElementSequenceWithMultiplePredicateMatches()
        {
            Assert.AreEqual(3, new[] { 3, 2, 1 }.First(x => x > 1));
        }

        [Test]
        public void EarlyOutAfterFirstElementWithoutPredicate()
        {
            var source = new[] {2, 0, 1};
            var query = source.Select(x => 10 / x);
            Assert.AreEqual(5, query.First());
        }

        [Test]
        public void EarlyOutAfterFirstElementWithPredicate()
        {
            var source = new[] { 2, 0, 1 };
            var query = source.Select(x => 10 / x);
            Assert.AreEqual(5, query.First(x => x >= 5));
        }
    }
}