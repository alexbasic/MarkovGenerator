using Generator.MarkovModels;
using Generator.PhraseGenerators;
using NUnit.Framework;

namespace GeneratorTest
{
    [TestFixture]
    public class MarkovGeneratorTest
    {
        [Test]
        public void PhraseGeneratorTest()
        {
            var one = new Word { Value = "one", IsStart = true };
            var two = new Word { Value = "two", IsStart = false };
            var three = new Word { Value = "three", IsStart = true };
            var four = new Word { Value = "four", IsStart = false };

            var model = new MarkovModel
            {
                Words = new Word[]
                {
                    one,
                    two,
                    three,
                    four
                },
                StartWords = new Word[]
                {
                    one,
                    three
                },
                WordLinks = new WordLink[]
                {
                    new WordLink { FromWord = one, ToWord = two, Weight = 100 },
                    new WordLink { FromWord = three, ToWord = four, Weight = 100 },
                    new WordLink { FromWord = two, ToWord = three, Weight = 10 },
                }
            };

            var generator = new MarkovPhraseGenerator(model, true);

            for (var index = 0; index < 100; index++)
            {
                var phrase = generator.GetPhrase();
                Assert.IsTrue(phrase.StartsWith("One two three") || 
                    phrase.StartsWith("Three four"));
            }
        }
    }
}
