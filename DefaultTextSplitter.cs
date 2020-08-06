using System;

namespace Generator
{
    public class DefaultTextSplitter : ITextSplitter
    {
        public string[] SplitBySentence(string text)
        {
            return text.Split(new char[] { '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);
        }

        public string[] SplitSentenceByWords(string sentence)
        {
            return sentence.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
