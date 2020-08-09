using Generator.MarkovModels.ModelGenerators;
using GeneratorTest.Comparers;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;

namespace GeneratorTest
{
    [TestFixture]
    public class MarkovModelTest
    {
        [Test]
        public void ModelTest()
        {
            var preparatorMock = new Mock<ITextPreparator>();
            preparatorMock.Setup(x => x.Prepare(It.IsAny<string>()))
                .Returns<string>(x => x.ToLower());
            var splitterMock = new Mock<ITextSplitter>();
            splitterMock.Setup(x => x.SplitBySentence(It.IsAny<string>()))
                .Returns<string>(x => x.Split('.', StringSplitOptions.RemoveEmptyEntries));
            splitterMock.Setup(x => x.SplitSentenceByWords(It.IsAny<string>()))
                .Returns<string>(x => x.Split(' ', StringSplitOptions.RemoveEmptyEntries));

            var text = "One two. One two three four. Two One. Three.";

            var markovModel = new MarkovModelGenerator(preparatorMock.Object, splitterMock.Object)
                .MakeFromText(text);

            //Words assert
            var actualWords = markovModel.Words.Select(x => x.Value);
            Assert.AreEqual(4, markovModel.Words.Length);
            CollectionAssert.Contains(actualWords, "one");
            CollectionAssert.Contains(actualWords, "two");
            CollectionAssert.Contains(actualWords, "three");
            CollectionAssert.Contains(actualWords, "four");
            CollectionAssert.AllItemsAreUnique(actualWords);
            Assert.IsTrue(markovModel.Words.Single(x => x.Value.Equals("one")).IsStart);
            Assert.IsTrue(markovModel.Words.Single(x => x.Value.Equals("two")).IsStart);
            Assert.IsTrue(markovModel.Words.Single(x => x.Value.Equals("three")).IsStart);
            Assert.IsFalse(markovModel.Words.Single(x => x.Value.Equals("four")).IsStart);

            //WordLinks assert
            CollectionAssert.AllItemsAreUnique(markovModel.WordLinks);
            Assert.AreEqual(markovModel.WordLinks.Count(), markovModel.WordLinks.Distinct(new WordLinkEqualityComparer()).Count());
            Assert.DoesNotThrow(() => markovModel.WordLinks
                .Single(x => x.FromWord.Value.Equals("one") && x.ToWord.Value.Equals("two") && x.Weight == 2));
            Assert.DoesNotThrow(() => markovModel.WordLinks
                .Single(x => x.FromWord.Value.Equals("two") && x.ToWord.Value.Equals("three") && x.Weight == 1));
            Assert.DoesNotThrow(() => markovModel.WordLinks
                .Single(x => x.FromWord.Value.Equals("two") && x.ToWord.Value.Equals("one") && x.Weight == 1));
            Assert.DoesNotThrow(() => markovModel.WordLinks
                .Single(x => x.FromWord.Value.Equals("three") && x.ToWord.Value.Equals("four") && x.Weight == 1));

            //StartWords assert
            var startWords = markovModel.StartWords.Select(x => x.Value).ToArray();
            CollectionAssert.AllItemsAreUnique(startWords);
            Assert.IsTrue(markovModel.StartWords.Select(x => x.IsStart).Distinct().Single());
            Assert.AreEqual(3, startWords.Length);
            CollectionAssert.AllItemsAreUnique(startWords);
            CollectionAssert.Contains(startWords, "one");
            CollectionAssert.Contains(startWords, "two");
            CollectionAssert.Contains(startWords, "three");
        }
    }
}