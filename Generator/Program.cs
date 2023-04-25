using Generator.MarkovModels.ModelGenerators;
using Generator.PhraseGenerators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace Generator
{
    class Program
    {
        static void Main(string[] args)
        {
            var (fileNames, useAltrnativeModelGenerator, phrasesCountGenerate) = ReadParameters(args);

            var inputText = fileNames
                .Select(path => File.ReadAllText(path))
                .Aggregate((a, path) => a + " " + path);
            phrasesCountGenerate = (phrasesCountGenerate > 0) ? phrasesCountGenerate : 10;

            var modelGenerator = useAltrnativeModelGenerator ?
                new MarkovModelGenerator(new AlternativeTextPreparator(), new AlternativeTextSplitter()) :
                new MarkovModelGenerator();
            var model = modelGenerator.MakeFromText(inputText);

            var generator = new MarkovPhraseGenerator(model, true);

            for (var index = 0; index < phrasesCountGenerate; index++)
            {
                Console.WriteLine(generator.GetPhrase());
            }
        }

        static (string[] FileNames, bool UseAltrnativeModelGenerator, int PhrasesCountGenerate) ReadParameters(string[] args)
        {
            var fileNames = args.Where(x => !x.StartsWith("--")).ToArray();
            var options = args
                .Where(x => x.StartsWith("--"))
                .Select(x => 
                    {
                        var delimiterIndex = x.IndexOf("=");
                        if (delimiterIndex > -1)
                        {
                            return new KeyValuePair<string, string>(
                                x.Substring(2, delimiterIndex),
                                x.Substring(delimiterIndex + 1)
                                );
                        }
                        else
                        {
                            return new KeyValuePair<string, string>(
                                x.Substring(2, delimiterIndex),
                                default(string)
                                );
                        }
                    })
                .ToArray();

            bool.TryParse(options.FirstOrDefault(x => x.Key == "UseAltrnativeModelGenerator").Value ?? "False", out bool useAltrnativeModelGeneratorValue);
            int.TryParse(options.FirstOrDefault(x => x.Key == "PhrasesCountGenerate").Value ?? "0", out int phrasesCountGenerate);

            return (FileNames: fileNames,
                UseAltrnativeModelGenerator: useAltrnativeModelGeneratorValue,
                PhrasesCountGenerate: phrasesCountGenerate);
        }
    }
}
