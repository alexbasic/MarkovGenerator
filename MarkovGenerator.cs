using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Generator
{
    public class MarkovGenerator
    {
        private MarkovModel _model;

        /// <summary>
        /// Выбрать следующе слово по распределению вероятностей (true) или случайно (false)
        /// </summary>
        private bool _nextWordByDistribution;

        public MarkovGenerator(MarkovModel model, bool nextWordByDistribution)
        {
            _model = model;
            _nextWordByDistribution = nextWordByDistribution;
        }

        public string GetPhrase()
        {
            var stringBuilder = new StringBuilder();

            var startWordNumber = new Random().Next(_model.StartWords.Count());

            var nextWord = _model.StartWords[startWordNumber];
            var firstWord = true;

            while (true)
            {
                var nextLinks = _model.WordLinks.Where(x => x.FromWord == nextWord).ToArray();

                var n = nextWord.Value;
                stringBuilder.Append(
                    (firstWord) ?
                    $"{Char.ToUpper(n[0])}{n.Substring(1, n.Length - 1)}" :
                    $"{n}");

                firstWord = false;

                if (nextLinks.Length == 0) break;

                stringBuilder.Append(" ");

                var prevWord = nextWord;
                nextWord = _nextWordByDistribution ? GetNextWordByDistribution(nextLinks) : GetRandomNextWord(nextLinks);

                if (nextWord == prevWord) throw new Exception($"{nextWord.Value} == {prevWord.Value}");
            }

            stringBuilder.Append(".");

            return stringBuilder.ToString();
        }

        private Word GetNextWordByDistribution(WordLink[] nextLinks)
        {
            var summ = nextLinks.Sum(x => x.Weight);
            var nextWordNumber = new Random().Next(summ + 1);
            var accum = 0;
            Word nextWord = null;
            foreach (var item in nextLinks)
            {
                accum += item.Weight;
                nextWord = item.ToWord;
                if (accum >= nextWordNumber)
                {
                    break;
                }
            }

            return nextWord;
        }

        private Word GetRandomNextWord(WordLink[] nextLinks)
        {
            var nextWordNumber = new Random().Next(nextLinks.Count());
            var nextWord = nextLinks[nextWordNumber].ToWord;

            return nextWord;
        }
    }
}
