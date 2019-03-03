using MyLinqImplementation;
using MyLinqTests.Helpers;
using NUnit.Framework;

namespace MyLinqTests
{
    [TestFixture()]
    public class SelectManyTests
    {
        [Test]
        public void SimpleFlatten()
        {
            var source = new[] {1, 2, 10, 15};
            source.SelectMany(x => x.ToString()).AssertSequenceEqual('1', '2', '1', '0', '1', '5');
        }

        [Test]
        public void SimpleFlattenWithIndex()
        {
            var source = new[] {1, 10};
            source.SelectMany((x, i) => (x + i).ToString()).AssertSequenceEqual('1', '1', '1');
        }

        [Test]
        public void FlattenWithProjection()
        {
            var source = new[] { 1, 2, 10};
            source.SelectMany(x => x.ToString(), (x, c) => x + "*" + c)
                .AssertSequenceEqual("1*1", "2*2", "10*1", "10*0");
        }

        [Test]
        public void FlattenWithProjectionAndIndex()
        {
            var source = new[] { 1, 2, 10 };
            source.SelectMany((x, i) => (x + i).ToString(), (x, c) => x + "*" + c)
                .AssertSequenceEqual("1*1", "2*3", "10*1", "10*2");
        }
    }
}