using System;
using System.Collections;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpellChecker;

namespace SpellCheckerTest
{
    [TestClass]
    public class SpellingCheckerTest
    {
        private const int FillFactor = 64;

        private static readonly int[] Primes = 
        {
            2, 3, 5, 7, 11, 13, 17, 19, 23, 29,
            31, 37, 41, 43, 47, 53, 59, 61, 67, 71,
            73, 79, 83, 89, 97, 101, 103, 107, 109, 113
        };

        private static BitArray GetBitArray(SpellingChecker spellingChecker)
        {
            var field = typeof(SpellingChecker).GetField("_bits", BindingFlags.NonPublic | BindingFlags.Instance);

            Contract.Assert(field != null);

            return field.GetValue(spellingChecker) as BitArray;
        }

        private static int ComputeHash(int size, string word, int hashId)
        {
            unchecked
            {
                return Math.Abs(word.GetHashCode() * Primes[hashId]) % size;
            }
        }

        [TestMethod]
        public void When_the_spelling_checker_is_created_the_bit_array_is_sized_properly_for_a_word_count_a_power_of_two()
        {
            const int wordCount = 32;
            const int hashCount = 4;

            const int expectedSize = wordCount * hashCount * FillFactor;

            var spellingChecker = new SpellingChecker(wordCount, hashCount);

            var bitArray = GetBitArray(spellingChecker);

            Assert.AreEqual(expectedSize, bitArray.Count);
        }

        [TestMethod]
        public void When_the_spelling_checker_is_created_the_bit_array_is_sized_properly_for_a_different_number_of_words()
        {
            const int wordCount = 33;
            const int hashCount = 4;

            const int expectedSize = wordCount * hashCount * FillFactor;

            var spellingChecker = new SpellingChecker(wordCount, hashCount);

            var bitArray = GetBitArray(spellingChecker);

            Assert.AreEqual(expectedSize, bitArray.Count);
        }

        [TestMethod]
        public void When_the_spelling_checker_is_created_the_bit_array_is_sized_properly_for_a_different_number_of_hashes()
        {
            const int wordCount = 32;
            const int hashCount = 6;

            const int expectedSize = wordCount * hashCount * FillFactor;

            var spellingChecker = new SpellingChecker(wordCount, hashCount);

            var bitArray = GetBitArray(spellingChecker);

            Assert.AreEqual(expectedSize, bitArray.Count);
        }

        [TestMethod]
        public void When_a_word_is_added_hash_count_bits_are_set()
        {
            const int wordCount = 32;
            const int hashCount = 4;
            const string word = "word";

            var spellingChecker = new SpellingChecker(wordCount, hashCount);

            spellingChecker.Add(word);

            var bitArray = GetBitArray(spellingChecker);

            var count = bitArray.Cast<bool>().Count(b => b);

            Assert.AreEqual(hashCount, count);
        }

        [TestMethod]
        public void When_a_word_is_added_hash_the_correct_bits_are_set()
        {
            const int wordCount = 32;
            const int hashCount = 4;
            const int bitArraySize = wordCount * hashCount * FillFactor;
            const string word = "word";

            var indexes = new int[hashCount];
            for (var i = 0; i < hashCount; ++i)
            {
                indexes[i] = ComputeHash(bitArraySize, word, i);
            }

            var spellingChecker = new SpellingChecker(wordCount, hashCount);

            spellingChecker.Add(word);

            var bitArray = GetBitArray(spellingChecker);

            foreach (var index in indexes)
            {
                Assert.IsTrue(bitArray[index]);
            }
        }

        [TestMethod]
        public void When_a_word_is_added_check_returns_true_for_that_word()
        {
            const int wordCount = 32;
            const int hashCount = 4;
            const string word = "word";

            var spellingChecker = new SpellingChecker(wordCount, hashCount);

            spellingChecker.Add(word);

            Assert.IsTrue(spellingChecker.Check(word));
        }

        [TestMethod]
        public void When_a_word_is_added_check_returns_true_for_that_word_regardless_of_casing()
        {
            const int wordCount = 32;
            const int hashCount = 4;
            const string word = "word";

            var spellingChecker = new SpellingChecker(wordCount, hashCount);

            spellingChecker.Add(word.ToUpper());

            Assert.IsTrue(spellingChecker.Check(word));
        }

        [TestMethod]
        public void When_a_word_is_not_added_check_returns_false_for_that_word()
        {
            const int wordCount = 32;
            const int hashCount = 4;
            const string word = "word";

            var spellingChecker = new SpellingChecker(wordCount, hashCount);

            Assert.IsFalse(spellingChecker.Check(word));
        }

        [TestMethod]
        public void When_a_word_is_added_check_returns_false_for_a_different_word()
        {
            const int wordCount = 32;
            const int hashCount = 4;
            const string word = "word";

            var spellingChecker = new SpellingChecker(wordCount, hashCount);

            spellingChecker.Add(word);

            Assert.IsFalse(spellingChecker.Check(word + "a"));
        }

        [TestMethod]
        public void When_enough_words_are_added_collisions_result_in_false_positives()
        {
            const int wordCount = 1;
            const int hashCount = 1;
            const string word = "word";

            var spellingChecker = new SpellingChecker(wordCount, hashCount);

            for (int c = 'a'; c < 'z'; ++c)
            {
                spellingChecker.Add(word + c);
            }

            Assert.IsTrue(spellingChecker.Check(word));
        }
    }
}
