using System.Collections.Generic;

namespace MyLinqTests.Helpers
{
    public class SimpleEqualityComparer : IEqualityComparer<object>
    {
        public new bool Equals(object x, object y)
        {
            return x.Equals(y);
        }

        public int GetHashCode(object obj)
        {
            return obj.GetHashCode();
        }
    }
}