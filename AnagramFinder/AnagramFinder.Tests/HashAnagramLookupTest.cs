using System;
using System.Collections.Generic;
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

            var anagrams = algorithm.FindAnagrams("snip", new[] { "word", "foo", "bar" }).ToList();

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
        public void When_the_word_list_contains_the_word_with_different_case_the_original_word_is_returned()
        {
            var algorithm = _c.GetAlgorithm();

            var anagrams = algorithm.FindAnagrams("SNIP", new[] { "snip", "foo", "bar" }).ToList();

            Assert.AreEqual(1, anagrams.Count);
            Assert.AreEqual("SNIP", anagrams[0]);
        }

        [TestMethod]
        public void When_the_word_list_contains_an_anagram_both_the_word_and_the_anagram_are_returned()
        {
            var algorithm = _c.GetAlgorithm();

            var anagrams = algorithm.FindAnagrams("SNIP", new[] { "pins", "foo", "bar" }).ToList();

            Assert.AreEqual(2, anagrams.Count);
            CollectionAssert.AreEquivalent(new List<string> { "SNIP", "pins" }, anagrams);
        }

        [TestMethod]
        public void When_the_word_list_contains_multiple_anagrams_for_a_word_all_are_returned()
        {
            var algorithm = _c.GetAlgorithm();

            var anagramList = WordListHelper.GetAnagramList();

            var anagrams = algorithm.FindAnagrams(anagramList[0], anagramList).ToList();

            Assert.AreEqual(anagramList.Count, anagrams.Count);
            CollectionAssert.AreEquivalent(anagramList, anagrams);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void When_a_null_word_list_is_given_when_finding_all_an_argument_null_exception_is_thrown()
        {
            var algorithm = _c.GetAlgorithm();

            algorithm.FindAnagrams(null);
        }

        [TestMethod]
        public void When_the_word_list_contains_an_anagram_only_it_is_returned()
        {
            var algorithm = _c.GetAlgorithm();

            var anagrams = algorithm.FindAnagrams(new[] { "snip", "pins", "foo", "bar" });

            Assert.AreEqual(1, anagrams.Count);
            CollectionAssert.AreEquivalent(new List<string> { "pins" }, anagrams["snip"].ToList());
        }

        [TestMethod]
        public void When_the_word_list_contains_multiple_anagrams_all_are_returned()
        {
            var algorithm = _c.GetAlgorithm();

            var anagrams = algorithm.FindAnagrams(new[] { "snip", "pins", "foo", "bar", "abet", "beat" });

            Assert.AreEqual(2, anagrams.Count);
            Assert.IsTrue(anagrams.Keys.Contains("snip"));
            Assert.IsTrue(anagrams.Keys.Contains("abet"));
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
