namespace Generator.MarkovModels.ModelGenerators
{
    public interface ITextSplitter
    {
        string[] SplitBySentence(string text);
        string[] SplitSentenceByWords(string sentence);
    }
}
