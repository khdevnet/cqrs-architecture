using System;
using System.Collections.Generic;

namespace CQRS.Socks.Order.Tests.Comparers
{
    public class GuidEqualityComparer : IEqualityComparer<Guid>
    {
        public bool Equals(Guid x, Guid y)
        {

            var s = x.ToString().CompareTo(y.ToString()) == 0;

            var s1 = x.ToString().StartsWith(y.ToString());
            return x.ToString().CompareTo(y.ToString()) == 0;
        }

        public int GetHashCode(Guid obj)
        {
            return obj.GetHashCode();
        }
    }
}
