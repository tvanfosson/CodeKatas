using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AnagramFinder.Tests
{
    
    
    /// <summary>
    ///This is a test class for HashedWordTest and is intended
    ///to contain all HashedWordTest Unit Tests
    ///</summary>
    [TestClass()]
    public class HashedWordTest
    {

        [TestMethod]
        public void When_two_word_contain_the_same_letters_they_hash_to_the_same_values()
        {

            var firstWord = new HashedWord("word");
            var secondWord = new HashedWord("drow");

            Assert.AreEqual(firstWord.GetHashCode(), secondWord.GetHashCode());
        }

        [TestMethod]
        public void When_two_word_contain_the_same_letters_they_hash_to_the_same_values_regardless_of_case()
        {
            var firstWord = new HashedWord("word");
            var secondWord = new HashedWord("WORD");

            Assert.AreEqual(firstWord.GetHashCode(), secondWord.GetHashCode());
        }
    }
}
