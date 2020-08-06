﻿namespace Generator
{
    public interface ITextSplitter
    {
        string[] SplitBySentence(string text);
        string[] SplitSentenceByWords(string sentence);
    }
}
