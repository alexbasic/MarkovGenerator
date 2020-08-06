using System;
using System.IO;
using System.Linq;
using System.Resources;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Threading;

namespace Generator
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputFile = @"E:\Users\aleksandr\Documents\inputText1.txt";
            var inputText = File.ReadAllText(inputFile) + " " + 
                File.ReadAllText(@"E:\Users\aleksandr\Documents\inputText2.txt") + " " +
                File.ReadAllText(@"E:\Users\aleksandr\Documents\inputText3.txt") + " " +
                File.ReadAllText(@"E:\Users\aleksandr\Documents\inputText4.txt");

            var model = new MarkovModel(new TestTextPreparator(), new TestTextSplitter());
            model.PrepareFromText(inputText);

            var generator = new MarkovGenerator(model, true);

            while (true)
            {
                Console.WriteLine(generator.GetPhrase());

                Thread.Sleep(1000);
            }


            Console.WriteLine("Hello World!");
        }
    }

    public class TestTextPreparator : ITextPreparator
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

    public class TestTextSplitter : ITextSplitter
    {
        public string[] SplitBySentence(string text)
        {
            return text.Split(new char[] { '.', '!', '?', ';', ',' }, StringSplitOptions.RemoveEmptyEntries);
        }

        public string[] SplitSentenceByWords(string sentence)
        {
            return sentence.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
