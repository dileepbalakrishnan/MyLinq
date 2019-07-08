using System;
using System.Collections.Generic;
using MyLinqImplementation;
using MyLinqTests.Helpers;
using NUnit.Framework;

namespace MyLinqTests
{
    [TestFixture]
    public class ToListTests
    {
        [Test]
        public void ResultIsIndependentOfSource()
        {
            var source = new List<string> {"a", "b", "c"};
            var result = source.ToList();
            source.AssertSequenceEqual(result);
            Assert.AreNotSame(result, source);
            source.Add("d");
            Assert.AreNotEqual(source.Count, result.Count);
        }

        [Test]
        public void SequenceIsEvaluatedEagerly()
        {
            var source = new[] {3, 2, 1, 0}.Select(x => 10 / x);
            Assert.Throws<DivideByZeroException>(() => source.ToList());
        }

        [Test]
        public void ConversionOfLazilyEvaluatedSequence()
        {
            var source = new[] {3, 2, 1}.Select(x => 6 / x);
            var result = source.ToList();
            result.AssertSequenceEqual(2, 3, 6);
        }

        [Test]
        public void CollectionOptimization()
        {
            var source = new NonEnumerableCollection<string> {"One", "Two"};
            var result = source.ToList();
            result.AssertSequenceEqual("One", "Two");
        }
    }
}