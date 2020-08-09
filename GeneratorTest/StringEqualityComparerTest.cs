using Generator.Comparers;
using NUnit.Framework;
using System;

namespace GeneratorTest
{
    [TestFixture]
    public class StringEqualityComparerTest
    {
        [Test]
        public void MustBeEquals()
        {
            var firstString = "Abc";
            var secondString = "Ab" + ((Func<string>)(() => "c"))();
            var isEquals = new StringEqualityComparer().Equals(firstString, secondString);
            Assert.IsTrue(isEquals);
        }

        [TestCase("Abc", null)]
        [TestCase(null, "Abc")]
        public void OneNullMustNotBeEquals(string firstString, string secondString)
        {
            var isEquals = new StringEqualityComparer().Equals(firstString, secondString);
            Assert.IsFalse(isEquals);
        }

        [Test]
        public void MustNotBeEquals()
        {
            var firstString = "Abc";
            var secondString = "Def";
            var isEquals = new StringEqualityComparer().Equals(firstString, secondString);
            Assert.IsFalse(isEquals);
        }
    }
}