using Generator.MarkovModels.ModelGenerators;
using NUnit.Framework;

namespace GeneratorTest
{
    [TestFixture]
    public class DefaultTextSplitterTest
    {
        [Test]
        public void SplitBySentenceTest()
        {
            var text = "One cat. Two words! Three trees? Four Doors.";
            var splitter = new DefaultTextSplitter();
            var splitedText = splitter.SplitBySentence(text);
            CollectionAssert.AllItemsAreUnique(splitedText);
            CollectionAssert.Contains(splitedText, "One cat");
            CollectionAssert.Contains(splitedText, "Two words");
            CollectionAssert.Contains(splitedText, "Three trees");
            CollectionAssert.Contains(splitedText, "Four Doors");
            Assert.AreEqual(4, splitedText.Length);
        }

        [Test]
        public void SplitByWordsTest()
        {
            var text = "One, two three four";
            var splitter = new DefaultTextSplitter();
            var splitedText = splitter.SplitSentenceByWords(text);
            CollectionAssert.AllItemsAreUnique(splitedText);
            CollectionAssert.Contains(splitedText, "One");
            CollectionAssert.Contains(splitedText, "two");
            CollectionAssert.Contains(splitedText, "three");
            CollectionAssert.Contains(splitedText, "four");
            Assert.AreEqual(4, splitedText.Length);
        }
    }
}
