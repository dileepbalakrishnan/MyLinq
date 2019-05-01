using System;
using MyLinqImplementation;
using MyLinqTests.Helpers;
using NUnit.Framework;

namespace MyLinqTests
{
    [TestFixture]
    public class JoinTests
    {
        [Test]
        public void CustomComparer()
        {
            var outer = new[] {"One", "Dull"};
            var inner = new[] {"Day", "onion"};
            var query = outer.Join(inner, o => o[0].ToString(), i => i[0].ToString(), (o, i) => o + " " + i,
                StringComparer.Ordinal);
            query.AssertSequenceEqual("Dull Day");
        }

        [Test]
        public void DifferentSourceTypes()
        {
            int[] outer = {5, 3, 7};
            string[] inner = {"bee", "giraffe", "tiger", "badger", "ox", "cat", "dog"};

            var query = outer.Join(inner,
                outerElement => outerElement,
                innerElement => innerElement.Length,
                (outerElement, innerElement) => outerElement + ":" + innerElement);
            query.AssertSequenceEqual("5:tiger", "3:bee", "3:cat", "3:dog", "7:giraffe");
        }

        [Test]
        public void ExecutionIsDeferred()
        {
            var outer = new ThrowingEnumerable();
            var inner = new ThrowingEnumerable();
            outer.Join(inner, x => x, y => y, (x, y) => x + y);
        }

        [Test]
        public void InnerSequenceIsBuffered()
        {
            var inner = new[] {1, 2, 0}.Select(x => x / x);
            var outer = new[] {1, 2, 0};
            var query = outer.Join(inner, o => o, i => i, (o, i) => o * i);
            using (var eneumerator = query.GetEnumerator())
            {
                Assert.Throws<DivideByZeroException>(() => eneumerator.MoveNext());
            }
        }

        [Test]
        public void NullKeys()
        {
            string[] outer = {"first", "null", "nothing", "second"};
            string[] inner = {"nuff", "second"};
            var query = outer.Join(inner,
                outerElement => outerElement.StartsWith("n") ? null : outerElement,
                innerElement => innerElement.StartsWith("n") ? null : innerElement,
                (outerElement, innerElement) => outerElement + ":" + innerElement);

            query.AssertSequenceEqual("second:second");
        }

        [Test]
        public void OuterSequenceIsStreamed()
        {
            var outer = new[] {1, 2, 0}.Select(x => x / x);
            var inner = new[] {1, 2, 0};
            var query = outer.Join(inner, o => o, i => i, (o, i) => o * i);
            using (var eneumerator = query.GetEnumerator())
            {
                Assert.IsTrue(eneumerator.MoveNext());
                Assert.AreEqual(1, eneumerator.Current);
                Assert.IsTrue(eneumerator.MoveNext());
                Assert.AreEqual(1, eneumerator.Current);
                Assert.Throws<DivideByZeroException>(() => eneumerator.MoveNext());
            }
        }

        [Test]
        public void SimpleJoin()
        {
            var outer = new[] {"One", "Dull"};
            var inner = new[] {"Day", "Onion"};
            var query = outer.Join(inner, o => o[0], i => i[0], (o, i) => o + " " + i);
            query.AssertSequenceEqual("One Onion", "Dull Day");
        }
    }
}