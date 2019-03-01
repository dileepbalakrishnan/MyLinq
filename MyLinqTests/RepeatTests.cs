using System;
using MyLinqImplementation;
using MyLinqTests.Helpers;
using NUnit.Framework;

namespace MyLinqTests
{
    [TestFixture]
    public class RepeatTests
    {
        [Test]
        public void EmptyRepeat()
        {
            Enumerable.Repeat("Hello", 0).AssertSequenceEqual();
        }

        [Test]
        public void NegativeCountThrowsArgumentException()
        {
            Assert.Throws(typeof(ArgumentException), () => Enumerable.Repeat("Hello", -1));
        }

        [Test]
        public void NullElement()
        {
            Enumerable.Repeat<string>(null, 2).AssertSequenceEqual(null, null);
        }

        [Test]
        public void SimpleRepeat()
        {
            Enumerable.Repeat("Hello", 2).AssertSequenceEqual("Hello", "Hello");
        }
    }
}