using System;
using System.Collections.Generic;
using MyLinqImplementation;
using NUnit.Framework;

namespace MyLinqTests
{
    [TestFixture()]
    public class AnyTests
    {
        [Test]
        public void NullSourceWithoutPredicate()
        {
            IEnumerable<int> source = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.Throws(typeof(ArgumentNullException), () => source.Any());
        }

        [Test]
        public void NullSourceWithPredicate()
        {
            IEnumerable<int> source = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.Throws(typeof(ArgumentNullException), () => source.Any(x => x > 2));

        }

        [Test]
        public void NullPredicate()
        {
            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.Throws(typeof(ArgumentNullException), () => Enumerable.Range(1, 5).Any(null));
        }

        [Test]
        public void EmptySequenceWithoutPredicate()
        {
            Assert.IsFalse(new int[0].Any());
        }

        [Test]
        public void EmptySequenceWithPredicate()
        {
            Assert.IsFalse(new int[0].Any(x => x > 2));
        }

        [Test]
        public void NonEmptySequenceWithoutPredicate()
        {
            Assert.IsTrue(new[] {1}.Any());
        }

        [Test]
        public void NonEmptySequenceWithPredicateMatchingElement()
        {
            Assert.IsTrue(new[] {3}.Any(x => x > 2));
        }

        [Test]
        public void NonEmptySequenceWithPredicateNotMatchingElement()
        {
            Assert.IsFalse(new[] {3}.Any(x => x < 2));
        }

        [Test]
        public void SequenceIsNotEvaluatedAfterFirstMatch()
        {
            Assert.IsTrue(new[] {4, 2}.Any(x => (x / (x - 2)) == 2));
        }

        [Test]
        public void SequenceIsNotEvaluatedAfterFirstMatch2()
        {
            var items = new[] {2, 0, 1};
            var query = items.Select(x => 10 / x);
            Assert.IsTrue(query.Any(x => x > 2));
        }
    }
}
