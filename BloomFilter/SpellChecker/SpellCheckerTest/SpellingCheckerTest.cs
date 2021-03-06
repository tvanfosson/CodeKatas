﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpellChecker;

namespace SpellCheckerTest
{
    [TestClass]
    public class SpellingCheckerTest
    {
        private const int FillFactor = 16;
        private IHashAlgorithm _hashAlgorithm;

        private int ComputeHash(int size, string word, int hashId)
        {
            unchecked
            {
                var bytes = word.SelectMany(c => Encoding.UTF8.GetBytes(new[] { char.ToLowerInvariant(c) }).Concat(BitConverter.GetBytes(hashId)));
                var hash = _hashAlgorithm.ComputeHash(bytes);
                return (int)(hash % size);
            }
        }

        private static BitArray GetBitArray(ISpellingChecker spellingChecker)
        {
            var field = typeof(SpellingChecker).GetField("_bits", BindingFlags.NonPublic | BindingFlags.Instance);

            Contract.Assert(field != null);

            return field.GetValue(spellingChecker) as BitArray;
        }

        private ISpellingChecker CreateSpellingChecker(int wordCount, int hashCount)
        {
            return new SpellingChecker(_hashAlgorithm, wordCount, hashCount);
        }

        [TestInitialize]
        public void Init()
        {
            _hashAlgorithm = new FNV1aHashAlgorithm();
        }

        [TestMethod]
        public void When_a_word_is_added_check_returns_false_for_a_different_word()
        {
            const int wordCount = 32;
            const int hashCount = 4;
            const string word = "word";

            var spellingChecker = CreateSpellingChecker(wordCount, hashCount);

            spellingChecker.Add(word);

            Assert.IsFalse(spellingChecker.Check(word + "a"));
        }

        [TestMethod]
        public void When_a_word_is_added_check_returns_true_for_that_word()
        {
            const int wordCount = 32;
            const int hashCount = 4;
            const string word = "word";

            var spellingChecker = CreateSpellingChecker(wordCount, hashCount);

            spellingChecker.Add(word);

            Assert.IsTrue(spellingChecker.Check(word));
        }

        [TestMethod]
        public void When_a_word_is_added_check_returns_true_for_that_word_regardless_of_casing()
        {
            const int wordCount = 32;
            const int hashCount = 4;
            const string word = "word";

            var spellingChecker = CreateSpellingChecker(wordCount, hashCount);

            spellingChecker.Add(word.ToUpper());

            Assert.IsTrue(spellingChecker.Check(word));
        }

        [TestMethod]
        public void When_a_word_is_added_hash_count_bits_are_set()
        {
            const int wordCount = 32;
            const int hashCount = 4;
            const string word = "word";

            var spellingChecker = CreateSpellingChecker(wordCount, hashCount);

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

            var spellingChecker = CreateSpellingChecker(wordCount, hashCount);

            spellingChecker.Add(word);

            var bitArray = GetBitArray(spellingChecker);

            foreach (var index in indexes)
            {
                Assert.IsTrue(bitArray[index]);
            }
        }

        [TestMethod]
        public void When_a_word_is_not_added_check_returns_false_for_that_word()
        {
            const int wordCount = 32;
            const int hashCount = 4;
            const string word = "word";

            var spellingChecker = CreateSpellingChecker(wordCount, hashCount);

            Assert.IsFalse(spellingChecker.Check(word));
        }

        [TestMethod]
        public void When_enough_words_are_added_collisions_result_in_false_positives()
        {
            const int wordCount = 1;
            const int hashCount = 1;
            const string word = "word";

            var spellingChecker = CreateSpellingChecker(wordCount, hashCount);

            for (int c = 'a'; c < 'z'; ++c)
            {
                spellingChecker.Add(word + c);
            }

            Assert.IsTrue(spellingChecker.Check(word));
        }

        [TestMethod]
        public void When_the_spelling_checker_is_created_the_bit_array_is_sized_properly()
        {
            const int wordCount = 32;
            const int hashCount = 4;

            const int expectedSize = wordCount * hashCount * FillFactor;

            var spellingChecker = CreateSpellingChecker(wordCount, hashCount);

            var bitArray = GetBitArray(spellingChecker);

            Assert.AreEqual(expectedSize, bitArray.Count);
        }

        [TestMethod]
        public void When_the_spelling_checker_is_created_the_bit_array_is_sized_properly_for_a_different_number_of_words()
        {
            const int wordCount = 33;
            const int hashCount = 4;

            const int expectedSize = wordCount * hashCount * FillFactor;

            var spellingChecker = CreateSpellingChecker(wordCount, hashCount);

            var bitArray = GetBitArray(spellingChecker);

            Assert.AreEqual(expectedSize, bitArray.Count);
        }

        [TestMethod]
        public void When_the_spelling_checker_is_created_the_bit_array_is_sized_properly_for_a_different_number_of_hashes()
        {
            const int wordCount = 32;
            const int hashCount = 6;

            const int expectedSize = wordCount * hashCount * FillFactor;

            var spellingChecker = CreateSpellingChecker(wordCount, hashCount);

            var bitArray = GetBitArray(spellingChecker);

            Assert.AreEqual(expectedSize, bitArray.Count);
        }
    }
}
