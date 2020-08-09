using Generator.MarkovModels;
using System.Collections.Generic;

namespace GeneratorTest.Comparers
{
    public class WordLinkEqualityComparer : IEqualityComparer<WordLink>
    {
        private static WordEqualityComparer _wordComparer = new WordEqualityComparer();

        public bool Equals(WordLink x, WordLink y)
        {
            if (x == y) return true;
            if (x == null || y == null) return false;
            
            return _wordComparer.Equals(x.FromWord, y.FromWord) &&
                _wordComparer.Equals(y.ToWord, y.ToWord) &&
                y.Weight == y.Weight;
        }

        public int GetHashCode(WordLink obj)
        {
            return obj.GetHashCode();
        }
    }
}