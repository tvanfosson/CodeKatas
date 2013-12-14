using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        public void When_the_word_list_contains_the_word_with_different_case_the_word_from_the_list_is_returned()
        {
            var algorithm = _c.GetAlgorithm();

            var anagrams = algorithm.FindAnagrams("SNIP", new[] { "snip", "foo", "bar" }).ToList();

            Assert.AreEqual(1, anagrams.Count);
            Assert.AreEqual("snip", anagrams[0]);
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
        public void When_the_word_list_contains_multiple_anagrams_both_all_are_returned()
        {
            var algorithm = _c.GetAlgorithm();

            var anagramList = WordListHelper.GetAnagramList();

            var anagrams = algorithm.FindAnagrams(anagramList[0], anagramList).ToList();

            Assert.AreEqual(anagramList.Count, anagrams.Count);
            CollectionAssert.AreEquivalent(anagramList, anagrams);
        }

        [TestMethod]
        public void When_two_word_contain_the_same_letters_they_hash_to_the_same_values()
        {
            var algorithm = _c.GetAlgorithm();
            var computeHashMethod = typeof(HashAnagramLookup).GetMethod("ComputeHash", BindingFlags.NonPublic|BindingFlags.Instance);

            var firstHash = (int)computeHashMethod.Invoke(algorithm, new object[] { "word" });
            var secondHash = (int)computeHashMethod.Invoke(algorithm, new object[] { "drow" });

            Assert.AreEqual(firstHash, secondHash);
        }

        [TestMethod]
        public void When_two_word_contain_the_same_letters_they_hash_to_the_same_values_regardless_of_case()
        {
            var algorithm = _c.GetAlgorithm();
            var computeHashMethod = typeof(HashAnagramLookup).GetMethod("ComputeHash", BindingFlags.NonPublic | BindingFlags.Instance);

            var firstHash = (int)computeHashMethod.Invoke(algorithm, new object[] { "word" });
            var secondHash = (int)computeHashMethod.Invoke(algorithm, new object[] { "WORD" });

            Assert.AreEqual(firstHash, secondHash);
        }

        [TestInitialize]
        public void Init()
        {
            _c = new HashAnagramLookupTestContext();
        }

        private class HashAnagramLookupTestContext
        {
            public HashAnagramLookup GetAlgorithm()
            {
                return new HashAnagramLookup();
            }
        }
    }
}
