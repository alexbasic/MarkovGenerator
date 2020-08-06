using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator
{
    public class MarkovModel
    {
        public Word[] Words { get; set; }
        public Word[] StartWords { get; private set; }
        public IEnumerable<WordLink> WordLinks { get; private set; }
        private ITextSplitter _textSplitter;
        private ITextPreparator _textPreparator;

        public MarkovModel()
        {
            _textSplitter = new DefaultTextSplitter();
            _textPreparator = new DefaultTextPreparator();
        }

        public MarkovModel(ITextPreparator preparator, ITextSplitter splitter)
        {
            _textSplitter = splitter;
            _textPreparator = preparator;
        }

        public void PrepareFromText(string inputText)
        {
            var preparedText = _textPreparator.Prepare(inputText);

            var inputWordSentence = _textSplitter.SplitBySentence(preparedText);
            var inputWordChains = inputWordSentence.Select(x => _textSplitter.SplitSentenceByWords(x));

            Words = inputWordChains.SelectMany(x =>
            {
                return x.Select(y => y);
            }).Distinct(new StringEqualityComparer()).Select(x => new Word { Value = x }).ToArray();

            var links = new List<WordLink>();

            foreach (var chain in inputWordChains)
            {
                var from = chain[0];
                var wordFrom = Words.Single(x => x.Value.Equals(from));
                for (var i = 1; i < chain.Length; i++)
                {
                    var to = chain[i];
                    var wordTo = Words.Single(x => x.Value.Equals(to));

                    //skip давайте - давайте
                    if (wordFrom != wordTo)
                    {

                        var link = links.SingleOrDefault(x => x.FromWord == wordFrom && x.ToWord == wordTo);
                        if (link == null)
                        {
                            link = new WordLink { FromWord = wordFrom, ToWord = wordTo };

                            links.Add(link);
                        }
                        link.Weight = link.Weight + 1;

                        if (i == 1)
                        {
                            link.FromWord.IsStart = true;
                        }

                        from = to;
                        wordFrom = wordTo;
                    }
                }
            }

            WordLinks = links;

            StartWords = Words.Where(x => x.IsStart).ToArray();
        }
    }
}
