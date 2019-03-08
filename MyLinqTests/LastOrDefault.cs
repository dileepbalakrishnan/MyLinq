using System;
using System.Collections.Generic;
using MyLinqImplementation;
using MyLinqTests.Helpers;
using NUnit.Framework;

namespace MyLinqTests
{
    [TestFixture]
    public class LastOrDefaultTests
    {
        [Test]
        public void NullSourceWithoutPredicate()
        {
            IEnumerable<int> source = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.Throws(typeof(ArgumentNullException), () => source.LastOrDefault());
        }

        [Test]
        public void NullSourceWithPredicate()
        {
            IEnumerable<int> source = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.Throws(typeof(ArgumentNullException), () => source.LastOrDefault(x => x > 2));
        }

        [Test]
        public void NullPredicate()
        {
            Assert.Throws(typeof(ArgumentNullException), () => new LinkedList<int>(new[] {1}).LastOrDefault(null));
        }

        [Test]
        public void EmptySequenceWithoutPredicate()
        {
            Assert.AreEqual(0, new LinkedList<int>(new int[0]).LastOrDefault());
        }

        [Test]
        public void SingleElementSequenceWithoutPredicate()
        {
            Assert.AreEqual(1, new LinkedList<int>(new [] {1}).LastOrDefault());
        }

        [Test]
        public void MultipleElementSequenceWithoutPredicate()
        {
            Assert.AreEqual(3, new LinkedList<int>(new[] { 1, 2, 3 }).LastOrDefault());
        }

        [Test]
        public void EmptySequenceWithPredicate()
        {
            Assert.AreEqual(0, new LinkedList<int>(new int[0]).LastOrDefault(x => x > 2));
        }

        [Test]
        public void SingleElementSequenceWithMatchingPredicate()
        {
            Assert.AreEqual(3, new LinkedList<int>(new [] {3}).LastOrDefault(x => x > 2));
        }

        [Test]
        public void SingleElementSequenceWithNonMatchingPredicate()
        {
            Assert.AreEqual(0, new LinkedList<int>(new[] {3}).LastOrDefault(x => x > 4));
        }

        [Test]
        public void MultipleElementSequenceWithNoPredicateMatches()
        {
            Assert.AreEqual(0, new LinkedList<int>(new[] {3, 2, 1}).LastOrDefault(x => x > 4));
        }

        [Test]
        public void MultipleElementSequenceWithSinglePredicateMatch()
        {
            Assert.AreEqual(3, new LinkedList<int>(new[] {3, 2, 1}).LastOrDefault(x => x > 2));
        }

        [Test]
        public void MultipleElementSequenceWithMultiplePredicateMatches()
        {
            Assert.AreEqual(2, new LinkedList<int>(new[] { 3, 2, 1 }).LastOrDefault(x => x > 1));
        }

        [Test]
        public void ListWithoutPredicateDoesntIterate()
        {
            var source = new NonEnumerableList<int>(new[] { 1, 2, 3 });
            Assert.AreEqual(3, source.LastOrDefault());
        }

        [Test]
        public void ListWithPredicateStillIterates()
        {
            var source = new NonEnumerableList<int>(new[] { 1, 2, 3 });
            Assert.Throws<NotSupportedException>(() => source.LastOrDefault(x => x > 2));
        }
    }
}