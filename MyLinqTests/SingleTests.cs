using System;
using System.Collections.Generic;
using MyLinqImplementation;
using NUnit.Framework;

namespace MyLinqTests
{
    [TestFixture]
    public class SingleTests
    {
        [Test]
        public void NullSourceWithoutPredicate()
        {
            IEnumerable<int> source = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.Throws(typeof(ArgumentNullException), () => source.Single());
        }

        [Test]
        public void NullSourceWithPredicate()
        {
            IEnumerable<int> source = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.Throws(typeof(ArgumentNullException), () => source.Single(x => x > 1));
        }

        [Test]
        public void NullPredicate()
        {
            Assert.Throws(typeof(ArgumentNullException), () => new[] {1}.Single(null));
        }

        [Test]
        public void EmptySequenceWithoutPredicate()
        {
            Assert.Throws(typeof(InvalidOperationException), () => new int[0].Single());
        }

        [Test]
        public void SingleElementSequenceWithoutPredicate()
        {
            Assert.AreEqual(1, new [] {1}.Single());
        }

        [Test]
        public void MultipleElementSequenceWithoutPredicate()
        {
            Assert.Throws<InvalidOperationException>(() => new[] {1, 2, 3}.Single());
        }

        [Test]
        public void EmptySequenceWithPredicate()
        {
            Assert.Throws<InvalidOperationException>(() => new int[0].Single(x => x > 2));
        }

        [Test]
        public void SingleElementSequenceWithMatchingPredicate()
        {
            Assert.AreEqual(3, new []{3}.Single(x => x>=3));
        }

        [Test]
        public void SingleElementSequenceWithNonMatchingPredicate()
        {
            Assert.Throws(typeof(InvalidOperationException), () => new[] {3}.Single(x => x > 3));
        }

        [Test]
        public void MultipleElementSequenceWithNoPredicateMatches()
        {
            Assert.Throws(typeof(InvalidOperationException), () => new[] { 3, 4, 5 }.Single(x => x > 5));
        }

        [Test]
        public void MultipleElementSequenceWithSinglePredicateMatch()
        {
            Assert.AreEqual(5, new[] {3, 4, 5}.Single(x => x > 4));
        }

        [Test]
        public void MultipleElementSequenceWithMultiplePredicateMatches()
        {
            Assert.Throws(typeof(InvalidOperationException), () => new[] { 3, 4, 5 }.Single(x => x > 3));
        }

        [Test]
        public void EarlyOutWithoutPredicate()
        {
            Assert.Throws(typeof(InvalidOperationException),
                () => new[] {3, 4, 0}.Select(x => 10 / x).Single());
        }

        [Test]
        public void EarlyOutWithPredicate()
        {
            Assert.Throws(typeof(InvalidOperationException),
                () => new[] { 3, 4, 0 }.Select(x => 12 / x).Single(x => x > 2));
        }
    }
}