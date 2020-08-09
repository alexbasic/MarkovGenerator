using System;
using System.Linq;

namespace Generator.MarkovModels.ModelGenerators
{
    public class DefaultTextSplitter : ITextSplitter
    {
        public string[] SplitBySentence(string text)
        {
            return text
                .Split(new char[] { '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .ToArray();
        }

        public string[] SplitSentenceByWords(string sentence)
        {
            return sentence.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
