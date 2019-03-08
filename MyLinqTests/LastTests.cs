using System;
using System.Collections.Generic;
using MyLinqImplementation;
using MyLinqTests.Helpers;
using NUnit.Framework;

namespace MyLinqTests
{
    [TestFixture]
    public class LastTests
    {
        [Test]
        public void NullSourceWithoutPredicate()
        {
            IEnumerable<int> source = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.Throws(typeof(ArgumentNullException), () => source.Last());
        }

        [Test]
        public void NullSourceWithPredicate()
        {
            IEnumerable<int> source = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.Throws(typeof(ArgumentNullException), () => source.Last(x => x > 2));
        }

        [Test]
        public void NullPredicate()
        {
            Assert.Throws(typeof(ArgumentNullException), () => new[] {1}.Last(null));
        }

        [Test]
        public void EmptySequenceWithoutPredicate()
        {
            Assert.Throws(typeof(InvalidOperationException), () => new int[0].Last());
        }

        [Test]
        public void SingleElementSequenceWithoutPredicate()
        {
            Assert.AreEqual(1, new[] {1}.Last());
        }

        [Test]
        public void MultipleElementSequenceWithoutPredicate()
        {
            Assert.AreEqual(3, new[] {1, 2, 3}.Last());
        }

        [Test]
        public void EmptySequenceWithPredicate()
        {
            Assert.Throws(typeof(InvalidOperationException), () => new int[0].Last(x => x > 2));
        }

        [Test]
        public void SingleElementSequenceWithMatchingPredicate()
        {
            Assert.AreEqual(3, new[] {3}.Last(x => x > 2));
        }

        [Test]
        public void SingleElementSequenceWithNonMatchingPredicate()
        {
            Assert.Throws(typeof(InvalidOperationException), () => new[] {3}.Last(x => x > 4));
        }

        [Test]
        public void MultipleElementSequenceWithNoPredicateMatches()
        {
            Assert.Throws(typeof(InvalidOperationException), () => new[] {3, 2, 1}.Last(x => x > 4));
        }

        [Test]
        public void MultipleElementSequenceWithSinglePredicateMatch()
        {
            Assert.AreEqual(3, new[] {3, 2, 1}.Last(x => x > 2));
        }

        [Test]
        public void MultipleElementSequenceWithMultiplePredicateMatches()
        {
            Assert.AreEqual(2, new[] {3, 2, 1}.Last(x => x > 1));
        }

        [Test]
        public void ListWithoutPredicateDoesntIterate()
        {
            var source = new NonEnumerableList<int>(new[] {1, 2, 3});
            Assert.AreEqual(3, source.Last());
        }

        [Test]
        public void ListWithPredicateStillIterates()
        {
            var source = new NonEnumerableList<int>(new[] { 1, 2, 3 });
            Assert.Throws<NotSupportedException>(() => source.Last(x => x > 2));
        }
    }
}