using MyLinqImplementation;
using NUnit.Framework;

namespace MyLinqTests
{
    [TestFixture]
    public class EmptyTests
    {
        [Test]
        public void EmptyContainsNoElements()
        {
            using (var empty = Enumerable.Empty<int>().GetEnumerator())
            {
                Assert.IsFalse(empty.MoveNext());
            }
        }

        [Test]
        public void EmptyIsASingletonPerElementType()
        {
            Assert.AreSame(Enumerable.Empty<int>(), Enumerable.Empty<int>());
            Assert.AreSame(Enumerable.Empty<string>(), Enumerable.Empty<string>());
            Assert.AreSame(Enumerable.Empty<object>(), Enumerable.Empty<object>());
        }
    }
}