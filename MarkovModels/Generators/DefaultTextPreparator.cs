using System.Text.RegularExpressions;

namespace Generator.MarkovModels.Generators
{
    public class DefaultTextPreparator : ITextPreparator
    {
        public string Prepare(string text)
        {
            var specChars = new Regex($"([,\\:\\-])", RegexOptions.Multiline);
            //var transChars = new Regex($"(\\S+)[\\s\r\\n]*-[\\s\r\n]*(\\S+)", RegexOptions.Multiline);
            var extraChars = new Regex($"[^a-zёа-я0-9 -!\\?\\.\\,]", RegexOptions.Multiline);

            var inputText = specChars.Replace(text.ToLower(), " ");
            inputText = extraChars.Replace(inputText, "");
            
            inputText = inputText
                .Replace("...", ".")
                .Replace("..", ".");

            return inputText;
        }
    }
}
