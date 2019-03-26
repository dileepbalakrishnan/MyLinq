using System;
using System.Collections.Generic;
using MyLinqImplementation;
using MyLinqTests.Helpers;
using NUnit.Framework;

namespace MyLinqTests
{
    [TestFixture]
    public class ExceptTests
    {
        [Test]
        public void CaseInsensitiveComparerSpecified()
        {
            new[] {"A", "B", "c"}.Except(new[] {"a", "b"}, StringComparer.OrdinalIgnoreCase).AssertSequenceEqual("c");
        }

        [Test]
        public void FirstSequenceOnlyReadAsResultsAreRead()
        {
            var first = new[] {5, 2, 0, 2}.Select(n => 10 / n);
            int[] second = {1, 2};
            var query = first.Except(second);
            using (var enumerator = query.GetEnumerator())
            {
                Assert.IsTrue(enumerator.MoveNext());
                Assert.AreEqual(5, enumerator.Current);
                Assert.Throws<DivideByZeroException>(() => enumerator.MoveNext());
            }
        }

        [Test]
        public void NoComparerSpecified()
        {
            new[] { "A", "B", "c" }.Except(new[] { "a", "b" }).AssertSequenceEqual("A", "B", "c");
        }

        [Test]
        public void NoSequencesUsedBeforeIteration()
        {
            var first = new ThrowingEnumerable();
            var second = new ThrowingEnumerable();
            var query = first.Except(second);
            using (var enumerator = query.GetEnumerator())
            {
            }
        }

        [Test]
        public void NullComparerSpecified()
        {
            new[] { "A", "B", "c" }.Except(new[] { "a", "b" }, null).AssertSequenceEqual("A", "B", "c");
        }

        [Test]
        public void NullFirstWithComparer()
        {
            IEnumerable<string> first = null;
            string[] second = { };
            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.Throws<ArgumentNullException>(() => first.Except(second, StringComparer.Ordinal));
        }

        [Test]
        public void NullFirstWithoutComparer()
        {
            IEnumerable<string> first = null;
            string[] second = { };
            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.Throws<ArgumentNullException>(() => first.Except(second));
        }

        [Test]
        public void NullSecondWithComparer()
        {
            IEnumerable<string> second = null;
            string[] first = { };
            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.Throws<ArgumentNullException>(() => first.Except(second, StringComparer.Ordinal));
        }

        [Test]
        public void NullSecondWithoutComparer()
        {
            IEnumerable<string> second = null;
            string[] first = { };
            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.Throws<ArgumentNullException>(() => first.Except(second));
        }

        [Test]
        public void SecondSequenceReadFullyOnFirstResultIteration()
        {
            int[] first = {3};
            var second = new[] {1, 3, 0}.Select(x => 10 / x);
            var query = first.Except(second);
            using (var enumerator = query.GetEnumerator())
            {
                Assert.Throws<DivideByZeroException>(() => enumerator.MoveNext());
            }
        }
    }
}