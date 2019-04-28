using System;
using System.Collections.Generic;
using MyLinqImplementation;
using MyLinqTests.Helpers;
using NUnit.Framework;

namespace MyLinqTests
{
    [TestFixture]
    public class ToLookupTests
    {
        [Test]
        public void SourceSequenceIsReadEagerly()
        {
            var source = new ThrowingEnumerable();
            Assert.Throws<InvalidOperationException>(() => source.ToLookup(x => x));
        }

        [Test]
        public void ChangesToSourceSequenceAfterToLookupAreIgnored()
        {
            List<string> source = new List<string> { "abc", "def" };
            var lookup = source.ToLookup(x => x.Length);
            Assert.AreEqual(1, lookup.Count);
            source.Add("gh"); // This item with new key (2) will be ignored
            Assert.AreEqual(1, lookup.Count);
            source.Add("ijk");  // This item with existing key (3) will also be ignored
            lookup[3].AssertSequenceEqual("abc", "def");
        }

        [Test]
        public void LookupWithNoComparerOrElementSelector()
        {
            string[] source = { "a", "bc", "def"};
            var lookup = source.ToLookup(value => value.Length);
            Assert.AreEqual(3, lookup.Count);
            lookup[1].AssertSequenceEqual("a");
            lookup[2].AssertSequenceEqual("bc");
            lookup[3].AssertSequenceEqual("def");
        }

        [Test]
        public void AnUnknownKeyWillReturnAnEmptySequence()
        {
            string[] source = { "a", "bc", "def" };
            var lookup = source.ToLookup(value => value.Length);
            lookup[99].AssertSequenceEqual();
        }

        [Test]
        public void LookupWithComparerButNoElementSelector()
        {
            string[] source = { "ABC", "abc", "def" };
            var lookup = source.ToLookup(value => value, StringComparer.OrdinalIgnoreCase);
            lookup["ABC"].AssertSequenceEqual("ABC", "abc");
            lookup["def"].AssertSequenceEqual("def");
        }

        [Test]
        public void LookupWithNullComparerButNoElementSelector()
        {
            string[] source = { "a", "def", "A" };
            var lookup = source.ToLookup(value => value, null);
            lookup["a"].AssertSequenceEqual("a");
            lookup["def"].AssertSequenceEqual("def");
            lookup["A"].AssertSequenceEqual("A");
        }

        [Test]
        public void LookupWithElementSelectorButNoComparer()
        {
            string[] source = { "a", "b", "cd", "ef"};
            var lookup = source.ToLookup(value => value.Length, value => value);
            lookup[1].AssertSequenceEqual("a", "b");
            lookup[2].AssertSequenceEqual("cd", "ef");
        }

        [Test]
        public void LookupWithComparareAndElementSelector()
        {
                string[] source = { "a", "b", "A", "B" };
                var lookup = source.ToLookup(value => value, value => value, StringComparer.OrdinalIgnoreCase);
                lookup["a"].AssertSequenceEqual("a", "A");
                lookup["b"].AssertSequenceEqual("b", "B");
    }

    [Test]
        public void FindByNullKeyNonePresent()
        {
            string[] source = { "first", "second" };
            var lookup = source.ToLookup(x => x);
            lookup[null].AssertSequenceEqual();
        }

        [Test]
        public void FindByNullKeyWhenPresent()
        {
            string[] source = { "first", "null", "nothing", "second" };
            var lookup = source.ToLookup(x => x.StartsWith("n") ? null : x);
            lookup[null].AssertSequenceEqual("null", "nothing");
            lookup.Select(x => x.Key).AssertSequenceEqual("first", null, "second");
            Assert.AreEqual(3, lookup.Count);
            lookup[null].AssertSequenceEqual("null", "nothing");
        }
    }
}