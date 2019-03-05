using System;
using MyLinqImplementation;
using NUnit.Framework;

namespace MyLinqTests
{
    [TestFixture]
    public class AllTests
    {
        [Test]
        public void NullSource()
        {
            int[] source = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.Throws<ArgumentNullException>(() => source.All(x => x > 2));
        }

        [Test]
        public void NullPredicate()
        {
            int[] source = {1, 2, 3};
            Assert.Throws<ArgumentNullException>(() => source.All(null));
        }

        [Test]
        public void EmptySequenceReturnsTrue()
        {
            Assert.IsTrue(new int[0].All(x => x > 2));
        }

        [Test]
        public void PredicateMatchingNoElements()
        {
            Assert.IsFalse(new[] {1, 2, 3}.All(x => x > 3));
        }

        [Test]
        public void PredicateMatchingSomeElements()
        {
            Assert.IsFalse(new[] {1, 2, 3}.All(x => x >= 2));
        }

        [Test]
        public void PredicateMatchingAllElements()
        {
            Assert.IsTrue(new[] {1, 2, 3}.All(x => x > 0));
        }

        [Test]
        public void SequenceIsNotEvaluatedAfterFirstNonMatch()
        {
            var items = new[] { 2, 0, 1 };
            var query = items.Select(x => 2 / x);
            Assert.IsFalse(query.All(x => x > 2));
        }
    }
}