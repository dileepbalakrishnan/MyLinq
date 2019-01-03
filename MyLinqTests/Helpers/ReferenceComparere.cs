using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace MyLinqTests.Helpers
{
    public class ReferenceComparere : IEqualityComparer<object>
    {
        public new bool Equals(object x, object y)
        {
            return ReferenceEquals(x, y);
        }

        public int GetHashCode(object obj)
        {
            return RuntimeHelpers.GetHashCode(obj);
        }
    }
}