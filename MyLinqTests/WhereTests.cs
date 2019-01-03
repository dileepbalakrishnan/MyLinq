using System;
using System.Collections.Generic;
using MyLinqImplementation;
using MyLinqTests.Helpers;
using NUnit.Framework;

namespace MyLinqTests
{
    [TestFixture]
    public class WhereTests
    {
        [Test]
        public void ExecutionIsDeferred()
        {
            ThrowingEnumerable.AssertDeferred(src => src.Where((x, i) => x > 0));
        }

        [Test]
        public void NullPredicateThrowsNullArgumentException()
        {
            int[] source = {1, 3, 7, 9, 10};
            Func<int, int, bool> predicate = null;
            Assert.Throws<ArgumentNullException>(() => source.Where(predicate));
        }

        [Test]
        public void NullSourceThrowsNullArgumentException()
        {
            IEnumerable<int> source = null;
            Assert.Throws<ArgumentNullException>(() => source.Where((x, i) => x > 5));
        }


        [Test]
        public void QueryExpressionSimpleFiltering()
        {
            int[] source = {1, 3, 4, 2, 8, 1};
            var result = from x in source
                where x < 4
                select x;
            result.AssertSequenceEqual(1, 3, 2, 1);
        }

        [Test]
        public void SimpleFiltering()
        {
            int[] source = {1, 3, 4, 2, 8, 1};
            var result = source.Where((x, i) => x < 4);
            result.AssertSequenceEqual(1, 3, 2, 1);
        }
    }
}