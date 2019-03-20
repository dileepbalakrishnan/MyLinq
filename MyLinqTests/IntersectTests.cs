using System;
using System.Collections.Generic;
using MyLinqImplementation;
using MyLinqTests.Helpers;
using NUnit.Framework;

namespace MyLinqTests
{
    [TestFixture]
    public class IntersectTests
    {
        [Test]
        public void NullFirstWithoutComparer()
        {
            IEnumerable<int> first = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.Throws<ArgumentNullException>(() => first.Intersect(new[] {1, 2}));
        }

        [Test]
        public void NullSecondWithoutComparer()
        {
            IEnumerable<int> second = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.Throws<ArgumentNullException>(() => new[] {1, 2}.Intersect(second));
        }

        [Test]
        public void NullFirstWithComparer()
        {
            IEnumerable<int> first = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.Throws<ArgumentNullException>(() => first.Intersect(new[] { 1, 2 }, EqualityComparer<int>.Default));
        }

        [Test]
        public void NullSecondWithComparer()
        {
            IEnumerable<int> second = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.Throws<ArgumentNullException>(() => new[] { 1, 2 }.Intersect(second, EqualityComparer<int>.Default));
        }

        [Test]
        public void NoComparerSpecified()
        {
            new[] {"a", "b"}.Intersect(new[] { "a", "B" }).AssertSequenceEqual("a");
        }

        [Test]
        public void NullComparerSpecified()
        {
            new[] { "a", "b" }.Intersect(new[] { "a", "B" }, null).AssertSequenceEqual("a");
        }

        [Test]
        public void CaseInsensitiveComparerSpecified()
        {
            new[] { "a", "b" }.Intersect(new[] { "a", "B" }, StringComparer.OrdinalIgnoreCase).AssertSequenceEqual("a", "b");
        }

        [Test]
        public void NoSequencesUsedBeforeIteration()
        {
            var first = new ThrowingEnumerable();
            var second = new ThrowingEnumerable();
            var query = first.Union(second);
            using (query.GetEnumerator())
            {
            }
        }

        [Test]
        public void SecondSequenceReadFullyOnFirstResultIteration()
        {
            int[] first = { 1 };
            var secondQuery = new[] { 10, 2, 0 }.Select(x => 10 / x);
            var query = first.Intersect(secondQuery);
            using (var enumerator = query.GetEnumerator())
            {
                Assert.Throws<DivideByZeroException>(() => enumerator.MoveNext());
            }
        }

        [Test]
        public void FirstSequenceOnlyReadAsResultsAreRead()
        {
            var firstQuery = new[] { 1, 2, 0}.Select(x => 4 / x);
            int[] second = { 2 };
            var query = firstQuery.Intersect(second);
            using (var enumerator = query.GetEnumerator())
            {
                Assert.IsTrue(enumerator.MoveNext());
                Assert.AreEqual(2, enumerator.Current);
                Assert.Throws<DivideByZeroException>(() => enumerator.MoveNext());
            }
        }
    }
}