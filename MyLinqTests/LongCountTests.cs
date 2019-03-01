using System;
using System.Collections.Generic;
using MyLinqImplementation;
using MyLinqTests.Helpers;
using NUnit.Framework;

namespace MyLinqTests
{
    [TestFixture]
    public class LongCountTests
    {
        [Test]
        public void CountOfNonCollectionSource()
        {
            Assert.AreEqual(5, Enumerable.Range(1, 5).LongCount());
        }

        [Test]
        public void LongCountOfGenericOnlyCollection()
        {
            Assert.AreEqual(5, new GenericOnlyCollection<int>(Enumerable.Range(0, 5)).LongCount());
        }

        [Test]
        public void LongCountOfSemiGenericCollection()
        {
            Assert.AreEqual(5, new SemiGenericCollection(Enumerable.Range(0, 5)).LongCount());
        }

        [Test]
        public void LongCountOfGenericCollection()
        {
            Assert.AreEqual(5, new List<int>(Enumerable.Range(0, 5)).LongCount());
        }

        [Test]
        public void NullSourceThrowsArgumentNullException()
        {
            IEnumerable<int> nullSource = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.Throws(typeof(ArgumentException), () => nullSource.LongCount());
        }

        [Test]
        public void PredicatedNullSourceThrowsArgumentNullException()
        {
            IEnumerable<int> nullSource = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.Throws(typeof(ArgumentException), () => nullSource.LongCount(x => x > 0));
        }

        [Test]
        public void PredicatedNullPredicateThrowsArgumentNullException()
        {
            Assert.Throws(typeof(ArgumentException), () => Enumerable.Range(0, 5).LongCount(null));
        }

        [Test]
        public void PredicatedLongCount()
        {
            Assert.AreEqual(2, Enumerable.Range(0, 5).LongCount(x => x % 2 > 0));
        }

        //[Test]
        //public void Overflow()
        //{
        //    var largeSequence = Enumerable.Range(0, int.MaxValue)
        //        .Concat(Enumerable.Range(0, 1));
        //    Assert.Throws<OverflowException>(() => largeSequence.Count());
        //}

        //[Test]
        //public void OverflowWithPredicate()
        //{
        //    var largeSequence = Enumerable.Range(0, int.MaxValue)
        //        .Concat(Enumerable.Range(0, 1));
        //    Assert.Throws<OverflowException>(() => largeSequence.Count(x => x >= 0));
        //}
    }
}