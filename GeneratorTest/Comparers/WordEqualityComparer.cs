using Generator.MarkovModels;
using System.Collections.Generic;

namespace GeneratorTest.Comparers
{
    public class WordEqualityComparer : IEqualityComparer<Word>
    {
        public bool Equals(Word x, Word y)
        {
            if (x == y) return true;
            if (x == null || y == null) return false;
            if (x.Value == y.Value) return true;
            if (x.Value == null || y.Value == null) return false;
            return x.Value.Equals(y.Value) && x.IsStart == y.IsStart;
        }

        public int GetHashCode(Word obj)
        {
            return obj.GetHashCode();
        }
    }
}