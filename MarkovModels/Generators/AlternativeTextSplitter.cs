using System;

namespace Generator.MarkovModels.Generators
{
    public class AlternativeTextSplitter : ITextSplitter
    {
        public string[] SplitBySentence(string text)
        {
            return text.Split(new char[] { '.', '!', '?', ';', ',' }, StringSplitOptions.RemoveEmptyEntries);
        }

        public string[] SplitSentenceByWords(string sentence)
        {
            return sentence.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
