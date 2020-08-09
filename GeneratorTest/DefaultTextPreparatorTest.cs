using Generator.MarkovModels.ModelGenerators;
using NUnit.Framework;

namespace GeneratorTest
{
    [TestFixture]
    public class DefaultTextPreparatorTest
    {
        [Test]
        public void PrepareTest()
        {
            var text = "@#$%^&One, (two) - three: four. One two.. One two... One! One? One-One. 1234567890.";
            var preparator = new DefaultTextPreparator();
            var preparedText = preparator.Prepare(text);
            Assert.AreEqual("one  two   three  four. one two. one two. one! one? one one. 1234567890.", preparedText);
        }
    }
}
