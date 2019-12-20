using System;
using MyLinqImplementation;
using MyLinqTests.Helpers;
using NUnit.Framework;

namespace MyLinqTests
{
    [TestFixture]
    public class TakeTests
    {
        [Test]
        public void CountEqualToSourceLength()
        {
            Enumerable.Range(1, 5).Take(5).AssertSequenceEqual(1,2,3,4,5);
        }

        [Test]
        public void CountGreaterThanSourceLength()
        {
            Enumerable.Range(1, 5).Take(6).AssertSequenceEqual(1, 2, 3, 4, 5);
        }

        [Test]
        public void CountShorterThanSource()
        {
            Enumerable.Range(1, 5).Take(4).AssertSequenceEqual(1, 2, 3, 4);
        }

        [Test]
        public void ExecutionIsDeferred()
        {
            new ThrowingEnumerable().Take(2);
        }

        [Test]
        public void NegativeCount()
        {
            Enumerable.Range(1, 5).Take(-2).AssertSequenceEqual();
        }

        [Test]
        public void NullSource()
        {
            Assert.Throws<ArgumentNullException>(() => ((string[]) null).Take(10));
        }

        [Test]
        public void OnlyEnumerateTheGivenNumberOfElements()
        {
            var source = new[] {1, 2, 0};
            var query = source.Select(x => 10 / x);
            query.Take(2).AssertSequenceEqual(10, 5);
        }
    }
}