using Generator.Generators;
using Generator.MarkovModels;
using Generator.MarkovModels.Generators;
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
            var inputText = File.ReadAllText(@"E:\Users\aleksandr\Documents\inputText1.txt") + " " + 
                File.ReadAllText(@"E:\Users\aleksandr\Documents\inputText2.txt") + " " +
                File.ReadAllText(@"E:\Users\aleksandr\Documents\inputText3.txt") + " " +
                File.ReadAllText(@"E:\Users\aleksandr\Documents\inputText4.txt");

            var useAltrnativeModelGenerator = true;

            //---------------------------------------

            var model = useAltrnativeModelGenerator ?
                new MarkovModel(new AlternativeTextPreparator(), new AlternativeTextSplitter()) :
                new MarkovModel();
            model.PrepareFromText(inputText);

            var generator = new MarkovGenerator(model, true);

            while (true)
            {
                Console.WriteLine(generator.GetPhrase());

                Thread.Sleep(1000);
            }
        }
    }
}
