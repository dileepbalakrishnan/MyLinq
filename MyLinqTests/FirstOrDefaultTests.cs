using System;
using System.Collections.Generic;
using MyLinqImplementation;
using NUnit.Framework;

namespace MyLinqTests
{
    [TestFixture]
    public class FirstOrDefaultTests
    {
        [Test]
        public void NullSourceWithoutPredicate()
        {
            IEnumerable<int> source = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.Throws(typeof(ArgumentNullException), () => source.FirstOrDefault());
        }

        [Test]
        public void NullSourceWithPredicate()
        {
            IEnumerable<int> source = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.Throws(typeof(ArgumentNullException), () => source.FirstOrDefault(x => x > 2));
        }

        [Test]
        public void NullPredicate()
        {
            Assert.Throws(typeof(ArgumentNullException), () => new[] {1}.FirstOrDefault(null));
        }

        [Test]
        public void EmptySequenceWithoutPredicate()
        {
            Assert.AreEqual(0, new int[0].FirstOrDefault());
        }

        [Test]
        public void SingleElementSequenceWithoutPredicate()
        {
            Assert.AreEqual(1, new [] {1}.FirstOrDefault());
        }

        [Test]
        public void MultipleElementSequenceWithoutPredicate()
        {
            Assert.AreEqual(1, new[] { 1, 2, 3 }.FirstOrDefault());
        }

        [Test]
        public void EmptySequenceWithPredicate()
        {
            Assert.AreEqual(0, new int[0].FirstOrDefault(x => x > 2));
        }

        [Test]
        public void SingleElementSequenceWithMatchingPredicate()
        {
            Assert.AreEqual(3, new [] {3}.FirstOrDefault(x => x > 2));
        }

        [Test]
        public void SingleElementSequenceWithNonMatchingPredicate()
        {
            Assert.AreEqual(0, new[] {3}.FirstOrDefault(x => x > 4));
        }

        [Test]
        public void MultipleElementSequenceWithNoPredicateMatches()
        {
            Assert.AreEqual(0, new[] {3, 2, 1}.FirstOrDefault(x => x > 4));
        }

        [Test]
        public void MultipleElementSequenceWithSinglePredicateMatch()
        {
            Assert.AreEqual(3, new[] {3, 2, 1}.FirstOrDefault(x => x > 2));
        }

        [Test]
        public void MultipleElementSequenceWithMultiplePredicateMatches()
        {
            Assert.AreEqual(3, new[] { 3, 2, 1 }.FirstOrDefault(x => x > 1));
        }

        [Test]
        public void EarlyOutAfterFirstElementWithoutPredicate()
        {
            var source = new[] {2, 0, 1};
            var query = source.Select(x => 10 / x);
            Assert.AreEqual(5, query.FirstOrDefault());
        }

        [Test]
        public void EarlyOutAfterFirstElementWithPredicate()
        {
            var source = new[] { 2, 0, 1 };
            var query = source.Select(x => 10 / x);
            Assert.AreEqual(5, query.FirstOrDefault(x => x >= 5));
        }
    }
}