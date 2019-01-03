using System.Collections.Generic;
using MyLinqImplementation;
using MyLinqTests.Helpers;
using NUnit.Framework;

namespace MyLinqTests
{
    [TestFixture]
    class SelectTests
    {
        [Test]
        public void SimpleProjectionToDifferentType()
        {
            int[] source = { 1, 2, 3 };

            IEnumerable<string> result = source.Select(x => x.ToString());

            result.AssertSequenceEqual("1", "2", "3");
        }

        [Test]
        public void SideEffectsInProjection()
        {
            var source = new int[3];

            int count = 0;

            var query = source.Select(x => count++);

            query.AssertSequenceEqual(0, 1, 2);

            query.AssertSequenceEqual(3, 4, 5);

            query.AssertSequenceEqual(6, 7, 8);
        }
    }
}
