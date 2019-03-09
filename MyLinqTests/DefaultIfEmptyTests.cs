using System;
using System.Collections.Generic;
using MyLinqImplementation;
using MyLinqTests.Helpers;
using NUnit.Framework;

namespace MyLinqTests
{
    [TestFixture]
    public class DefaultIfEmptyTests
    {
        [Test]
        public void EmptySequenceNoDefaultValue()
        {
            new int[0].DefaultIfEmpty().AssertSequenceEqual(0);
        }

        [Test]
        public void EmptySequenceWithDefaultValue()
        {
            new int[0].DefaultIfEmpty(1).AssertSequenceEqual(1);
        }

        [Test]
        public void NonEmptySequenceNoDefaultValue()
        {
            new [] {1, 2, 3}.DefaultIfEmpty().AssertSequenceEqual(1, 2, 3);
        }

        [Test]
        public void NonEmptySequenceWithDefaultValue()
        {
            new[] { 1, 2, 3 }.DefaultIfEmpty(1).AssertSequenceEqual(1, 2, 3);
        }

        [Test]
        public void NullSourceNoDefaultValue()
        {
            IEnumerable<int> source = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.Throws(typeof(ArgumentNullException), () => source.DefaultIfEmpty());
        }

        [Test]
        public void NullSourceWithDefaultValue()
        {
            IEnumerable<int> source = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.Throws(typeof(ArgumentNullException), () => source.DefaultIfEmpty(1));
        }
    }
}