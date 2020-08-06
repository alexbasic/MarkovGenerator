using System.Text.RegularExpressions;

namespace Generator.MarkovModels.Generators
{
    public class AlternativeTextPreparator : ITextPreparator
    {
        public string Prepare(string text)
        {
            var specChars = new Regex($"([\\*\\-])", RegexOptions.Multiline);
            var extraChars = new Regex($"[^a-zёа-я0-9 -!\\?\\.\\,]", RegexOptions.Multiline);

            var inputText = specChars.Replace(text.ToLower(), " ");
            inputText = extraChars.Replace(inputText, " ");

            inputText = inputText
                .Replace("...", ".")
                .Replace("..", ".");

            return inputText;
        }
    }
}
