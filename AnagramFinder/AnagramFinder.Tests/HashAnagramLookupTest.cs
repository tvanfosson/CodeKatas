using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AnagramFinder.Tests
{
    [TestClass]
    public class HashAnagramLookupTest
    {
        private HashAnagramLookupTestContext _c;

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void When_a_null_or_empty_word_is_given_an_argument_null_exception_is_thrown()
        {
            var algorithm = _c.GetAlgorithm();

            algorithm.FindAnagrams(null, new[] { "word" });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void When_a_null_word_list_is_given_an_argument_null_exception_is_thrown()
        {
            var algorithm = _c.GetAlgorithm();

            algorithm.FindAnagrams("word", null);
        }

        [TestMethod]
        public void When_an_empty_word_list_is_provided_only_the_word_is_returned()
        {
            var algorithm = _c.GetAlgorithm();

            var anagrams = algorithm.FindAnagrams("word", new string[0]).ToList();

            Assert.AreEqual(1, anagrams.Count);
            Assert.AreEqual("word", anagrams[0], true);
        }

        [TestMethod]
        public void When_the_word_list_contains_no_anagrams_only_the_word_is_returned()
        {
            var algorithm = _c.GetAlgorithm();

            var anagrams = algorithm.FindAnagrams("snip", new [] { "word", "foo", "bar" }).ToList();

            Assert.AreEqual(1, anagrams.Count);
            Assert.AreEqual("snip", anagrams[0], true);
        }

        [TestMethod]
        public void When_the_word_list_contains_the_word_itself_as_the_only_anagram_only_the_word_is_returned()
        {
            var algorithm = _c.GetAlgorithm();

            var anagrams = algorithm.FindAnagrams("snip", new[] { "snip", "foo", "bar" }).ToList();

            Assert.AreEqual(1, anagrams.Count);
            Assert.AreEqual("snip", anagrams[0], true);
        }

        [TestMethod]
        public void When_the_word_list_contains_the_word_with_different_case_the_word_from_the_list_is_returned()
        {
            var algorithm = _c.GetAlgorithm();

            var anagrams = algorithm.FindAnagrams("SNIP", new[] { "snip", "foo", "bar" }).ToList();

            Assert.AreEqual(1, anagrams.Count);
            Assert.AreEqual("snip", anagrams[0]);
        }

        [TestInitialize]
        public void Init()
        {
            _c = new HashAnagramLookupTestContext();
        }

        private class HashAnagramLookupTestContext
        {
            public IAnagramLookup GetAlgorithm()
            {
                return new HashAnagramLookup();
            }
        }
    }
}
