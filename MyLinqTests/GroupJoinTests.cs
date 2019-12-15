using System;
using System.Collections.Generic;
using System.Linq;
using MyLinqTests.Helpers;
using NUnit.Framework;

namespace MyLinqTests
{
    [TestFixture]
    public class GroupJoinTests
    {
        [Test]
        public void ExecutionIsDeferred()
        {
            new ThrowingEnumerable().GroupJoin(new ThrowingEnumerable(), x => x, y => y, (x, y) => x + y.Count());
        }

        [Test]
        public void SimpleGroupJoin()
        {
            var outer = new [] {"one", "two", "three"};
            var inner = new [] {"two", "one"};
            var result = outer.GroupJoin(inner, x => x, y => y, (x, y) => x + ":" + string.Join(";", y));
            result.AssertSequenceEqual(new List<string> {"one:one", "two:two", "three:"});
        }

        [Test]
        public void CustomComparer()
        {
            var outer = new [] { "One", "two", "three" };
            var inner = new [] { "two", "one" };
            var result = outer.GroupJoin(inner, x => x, y => y, (x, y) => x + ":" + string.Join(";", y), StringComparer.Ordinal);
            result.AssertSequenceEqual(new List<string> { "One:", "two:two", "three:" });
        }

        [Test]
        public void CustomComparer2()
        {
            var outer = new[] { "One", "two", "three" };
            var inner = new[] { "two", "one" };
            var result = outer.GroupJoin(inner, x => x, y => y, (x, y) => x + ":" + string.Join(";", y), StringComparer.OrdinalIgnoreCase);
            result.AssertSequenceEqual(new List<string> { "One:one", "two:two", "three:" });
        }

        [Test]
        public void NullKeys()
        {
            var outer = new[] { "One", "two", null };
            var inner = new[] { "two", "one" };
            var result = outer.GroupJoin(inner, x => x, y => y, (x, y) => x + ":" + string.Join(";", y), StringComparer.OrdinalIgnoreCase);
            result.AssertSequenceEqual(new List<string> {"One:one", "two:two", ":" });
        }
    }
}