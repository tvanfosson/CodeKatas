using System;
using System.Collections;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpellChecker;

namespace SpellCheckerTest
{
    [TestClass]
    public class SpellingCheckerTest
    {
        [TestMethod]
        public void When_the_spelling_checker_is_created_the_bit_array_is_sized_properly_for_a_word_count_a_power_of_two()
        {
            const int wordCount = 32;
            const int hashCount = 4;

            const int expectedSize = 32*4*4;

            var spellingChecker = new SpellingChecker(wordCount, hashCount);

            var bitArray = typeof (SpellingChecker).GetField("_bits", BindingFlags.NonPublic | BindingFlags.Instance)
                                                   .GetValue(spellingChecker) as BitArray;

            Assert.AreEqual(expectedSize,bitArray.Count);
        }

        [TestMethod]
        public void When_the_spelling_checker_is_created_the_bit_array_is_sized_properly_for_a_word_count_not_a_power_of_two()
        {
            const int wordCount = 33;
            const int hashCount = 4;

            const int expectedSize = 64 * 4 * 4;

            var spellingChecker = new SpellingChecker(wordCount, hashCount);

            var bitArray = typeof(SpellingChecker).GetField("_bits", BindingFlags.NonPublic | BindingFlags.Instance)
                                                   .GetValue(spellingChecker) as BitArray;

            Assert.AreEqual(expectedSize, bitArray.Count);
        }
    }
}
