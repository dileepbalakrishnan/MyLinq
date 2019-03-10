using System;
using System.Collections.Generic;
using MyLinqImplementation;
using NUnit.Framework;

namespace MyLinqTests
{
    [TestFixture]
    public class AggregateTests
    {
        [Test]
        public void NullSourceUnseeded()
        {
            IEnumerable<int> source = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.Throws<ArgumentNullException>(() => source.Aggregate((x, y) => x + y));
        }

        [Test]
        public void NullFuncUnseeded()
        {
            int[] source = { 1, 1 };
            Assert.Throws<ArgumentNullException>(() => source.Aggregate(null));
        }

        [Test]
        public void UnseededAggregation()
        {
            int[] source = { 1, 2, 3 };
            // First iteration: 1 * 3 + 2 = 5
            // Second iteration: 5 * 3 + 3 = 18
            Assert.AreEqual(18, source.Aggregate((current, value) => current * 3 + value));
        }

        [Test]
        public void NullSourceSeeded()
        {
            IEnumerable<int> source = null;
            Assert.Throws(typeof(ArgumentNullException), () => source.Aggregate(1, (x, y) => x + y));
        }

        [Test]
        public void NullFuncSeeded()
        {
            int[] source = { 1, 2 };
            Assert.Throws<ArgumentNullException>(() => source.Aggregate(5, null));
        }

        [Test]
        public void SeededAggregation()
        {
            int[] source = { 1, 2, 3 };
            // First iteration: 2 + 3 + 1 = 7
            // Second iteration: 7 * 3 + 2 = 23
            // Third iteration: 23 * 3 + 3 = 72
            Assert.AreEqual(72, source.Aggregate(2, (current, value) => current * 3 + value));
        }

        [Test]
        public void NullSourceSeededWithResultSelector()
        {
            int[] source = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.Throws(typeof(ArgumentNullException), () => source.Aggregate(2, (x, y) => x + y, z => z));
        }

        [Test]
        public void NullFuncSeededWithResultSelector()
        {
            int[] source = {1, 2};
            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.Throws(typeof(ArgumentNullException), () => source.Aggregate(2, null, z => z));
        }

        [Test]
        public void NullProjectionSeededWithResultSelector()
        {
            int[] source = { 1, 2 };
            Func<int, string> resultSelector = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.Throws(typeof(ArgumentNullException), () => source.Aggregate(2, (x, y) => x + y, resultSelector));
        }

        [Test]
        public void SeededAggregationWithResultSelector()
        {
            int[] source = { 1, 2, 3 };
            // First iteration: 2 + 3 + 1 = 7
            // Second iteration: 7 * 3 + 2 = 23
            // Third iteration: 23 * 3 + 3 = 72
            // Result Selector => "72"
            Assert.AreEqual("72", source.Aggregate(2, (current, value) => current * 3 + value, x => x.ToString()));
        }

        [Test]
        public void DifferentSourceAndAccumulatorTypes()
        {
            int[] source = { int.MaxValue,int.MaxValue};
            long sum = source.Aggregate(0L, (acc, value) => acc + value);
            Assert.AreEqual(4294967294L, sum);
            Assert.IsTrue(sum > int.MaxValue);
        }

        [Test]
        public void EmptySequenceUnseeded()
        {
            Assert.Throws<InvalidOperationException>(() => new int[0].Aggregate((x, y) => x + y));
        }

        [Test]
        public void EmptySequenceSeeded()
        {
            Assert.AreEqual(1, new int[0].Aggregate(1, (x, y) => x + y));
        }

        [Test]
        public void EmptySequenceSeededWithResultSelector()
        {
            Assert.AreEqual("1", new int[0].Aggregate(1, (x, y) => x + y, x => x.ToString()));
        }

        [Test]
        public void FirstElementOfInputIsUsedAsSeedForUnseededOverload()
        {
            int[] source = { 5, 3, 2 };
            // Seed  = 5
            // Accumulator = 5 * 3 = 15
            // Accumulator = 15 * 2 = 30
            Assert.AreEqual(30, source.Aggregate((acc, value) => acc * value));
        }
    }
}