using System;
using System.Collections.Generic;
using MyLinqImplementation;
using MyLinqTests.Helpers;
using NUnit.Framework;

namespace MyLinqTests
{
    [TestFixture]
    public class CountTests
    {
        [Test]
        public void CountOfNonCollectionSource()
        {
            Assert.AreEqual(5, Enumerable.Range(1, 5).Count());
        }

        [Test]
        public void CountOfGenericOnlyCollection()
        {
            Assert.AreEqual(5, new GenericOnlyCollection<int>(Enumerable.Range(0, 5)).Count);
        }

        [Test]
        public void CountOfSemiGenericCollection()
        {
            Assert.AreEqual(5, new SemiGenericCollection(Enumerable.Range(0, 5)).Count);
        }

        [Test]
        public void CountOfGenericCollection()
        {
            Assert.AreEqual(5, new List<int>(Enumerable.Range(0, 5)).Count);
        }

        [Test]
        public void NullSourceThrowsArgumentNullException()
        {
            IEnumerable<int> nullSource = null;
            Assert.Throws(typeof(ArgumentException), () => nullSource.Count());
        }

        [Test]
        public void PredicatedNullSourceThrowsArgumentNullException()
        {
            IEnumerable<int> nullSource = null;
            Assert.Throws(typeof(ArgumentException), () => nullSource.Count(x => x > 0));
        }

        [Test]
        public void PredicatedNullPredicateThrowsArgumentNullException()
        {
            Assert.Throws(typeof(ArgumentException), () => Enumerable.Range(0, 5).Count(null));
        }

        [Test]
        public void PredicatedCount()
        {
            Assert.AreEqual(2, Enumerable.Range(0, 5).Count(x => x % 2 > 0));
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