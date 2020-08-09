using Generator.Comparers;
using System.Collections.Generic;
using System.Linq;

namespace Generator.MarkovModels.ModelGenerators
{
    public class MarkovModelGenerator
    {
        private ITextSplitter _textSplitter;
        private ITextPreparator _textPreparator;

        public MarkovModelGenerator()
        {
            _textSplitter = new DefaultTextSplitter();
            _textPreparator = new DefaultTextPreparator();
        }

        public MarkovModelGenerator(ITextPreparator preparator, ITextSplitter splitter)
        {
            _textSplitter = splitter;
            _textPreparator = preparator;
        }

        public MarkovModel MakeFromText(string inputText)
        {
            var model = new MarkovModel();

            var preparedText = _textPreparator.Prepare(inputText);

            var inputWordSentence = _textSplitter.SplitBySentence(preparedText);
            var inputWordChains = inputWordSentence.Select(x => _textSplitter.SplitSentenceByWords(x));

            model.Words = inputWordChains.SelectMany(x =>
            {
                return x.Select(y => y);
            }).Distinct(new StringEqualityComparer()).Select(x => new Word { Value = x }).ToArray();

            var links = new List<WordLink>();

            foreach (var chain in inputWordChains)
            {
                var from = chain[0];
                var wordFrom = model.Words.Single(x => x.Value.Equals(from));
                wordFrom.IsStart = true;
                for (var i = 1; i < chain.Length; i++)
                {
                    var to = chain[i];
                    var wordTo = model.Words.Single(x => x.Value.Equals(to));

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

                        from = to;
                        wordFrom = wordTo;
                    }
                }
            }

            model.WordLinks = links;

            model.StartWords = model.Words.Where(x => x.IsStart).ToArray();

            return model;
        }
    }
}
