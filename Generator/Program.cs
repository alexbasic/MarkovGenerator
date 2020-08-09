using Generator.MarkovModels.ModelGenerators;
using Generator.PhraseGenerators;
using System;
using System.IO;

namespace Generator
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputText =
                File.ReadAllText(@"E:\Users\aleksandr\Documents\inputText1.txt") + " " +
                File.ReadAllText(@"E:\Users\aleksandr\Documents\inputText2.txt") + " " +
                File.ReadAllText(@"E:\Users\aleksandr\Documents\inputText3.txt") + " " +
                File.ReadAllText(@"E:\Users\aleksandr\Documents\inputText4.txt");

            var useAltrnativeModelGenerator = true;
            var phrasesCount = 100;

            var modelGenerator = useAltrnativeModelGenerator ?
                new MarkovModelGenerator(new AlternativeTextPreparator(), new AlternativeTextSplitter()) :
                new MarkovModelGenerator();
            var model = modelGenerator.MakeFromText(inputText);

            var generator = new MarkovPhraseGenerator(model, true);

            for (var index = 0; index < phrasesCount; index++)
            {
                Console.WriteLine(generator.GetPhrase());
            }
        }
    }
}
