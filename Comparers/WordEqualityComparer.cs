using Generator.MarkovModels;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Generator.Comparers
{
    public class WordEqualityComparer : IEqualityComparer<Word>
    {
        public bool Equals([AllowNull] Word x, [AllowNull] Word y)
        {
            if (x == y) return true;
            if (x == null || y == null) return false;
            return x.Value.Equals(y.Value);
        }

        public int GetHashCode([DisallowNull] Word obj)
        {
            return obj.GetHashCode();
        }
    }
}
