using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MyLinqImplementation;
using MyLinqTests.Helpers;
using NUnit.Framework;

namespace MyLinqTests
{
    [TestFixture]
    public class DistinctTests
    {
        private static readonly string TestString1 = "test";
        private static readonly string TestString2 = new string(TestString1.ToCharArray());
        [Test]
        public void NullSourceNoComparer()
        {
            IEnumerable<int> source = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.Throws(typeof(ArgumentNullException), () => source.Distinct());
        }

        [Test]
        public void NullSourceWithComparer()
        {
            IEnumerable<int> source = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.Throws(typeof(ArgumentNullException), () => source.Distinct(EqualityComparer<int>.Default));
        }

        [Test]
        public void NullElementsArePassedToComparer()
        {
            IEqualityComparer<object> comparer = new SimpleEqualityComparer();
            Assert.Throws<NullReferenceException>(() => comparer.GetHashCode(null));
            Assert.Throws<NullReferenceException>(() => comparer.Equals(null, "xyz"));
            string[] source = { "xyz", null, "xyz", null, "abc" };
            var distinct = source.Distinct(comparer);
            Assert.Throws<NullReferenceException>(() => distinct.Count());
        }

        [Test]
        public void HashSetCopesWithNullElementsIfComparerDoes()
        {
            IEqualityComparer<string> comparer = EqualityComparer<string>.Default;
            Assert.AreEqual(comparer.GetHashCode(null), comparer.GetHashCode(null));
            Assert.IsTrue(comparer.Equals(null, null));
            string[] source = { "xyz", null, "xyz", null, "abc" };
            source.Distinct(comparer).AssertSequenceEqual("xyz", null, "abc");
        }

        [Test]
        public void NoComparerSpecifiedUsesDefault()
        {
            new[] {"abc", TestString1, "ABC", TestString2, "def"}.Distinct()
                .AssertSequenceEqual("abc", TestString1, "ABC", "def");
        }

        [Test]
        public void NullComparerUsesDefault()
        {
            new[] { "abc", TestString1, "ABC", TestString2, "def" }.Distinct(null)
                .AssertSequenceEqual("abc", TestString1, "ABC", "def");
        }

        [Test]
        public void DistinctStringsWithCaseInsensitiveComparer()
        {
            new[] {"xyz", TestString1, "XYZ", TestString2, "def"}.Distinct(StringComparer.OrdinalIgnoreCase)
                .AssertSequenceEqual("xyz", TestString1, "def");
        }

        [Test]
        public void DistinctStringsCustomComparer()
        {
            // This time we'll make sure that TestString1 and TestString2 are treated differently
            string[] source = { "xyz", TestString1, "XYZ", TestString2, TestString1 };
            source.Distinct(new ReferenceEqualityComparer())
                  .AssertSequenceEqual("xyz", TestString1, "XYZ", TestString2);
        }

        // Implementation of IEqualityComparer[T] which uses object identity
        private class ReferenceEqualityComparer : IEqualityComparer<object>
        {
            // Use explicit interface implementation to avoid warnings about hiding
            // the static object.Equals(object, object)
            bool IEqualityComparer<object>.Equals(object x, object y)
            {
                return ReferenceEquals(x, y);
            }

            public int GetHashCode(object obj)
            {
                return RuntimeHelpers.GetHashCode(obj);
            }
        }

        // Implementation of IEqualityComparer[T] which uses object's Equals/GetHashCode methods
        // in the simplest possible way, without any attempt to guard against NullReferenceException.
        private class SimpleEqualityComparer : IEqualityComparer<object>
        {
            // Use explicit interface implementation to avoid warnings about hiding
            // the static object.Equals(object, object)
            bool IEqualityComparer<object>.Equals(object x, object y)
            {
                return x.Equals(y);
            }

            public int GetHashCode(object obj)
            {
                return obj.GetHashCode();
            }
        }
    }
}