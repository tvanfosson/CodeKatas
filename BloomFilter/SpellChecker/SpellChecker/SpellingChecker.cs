using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace SpellChecker
{
    public class SpellingChecker : ISpellingChecker
    {
        private const int FillFactor = 16;

        private readonly BitArray _bits;
        private readonly int _hashCount;
        private readonly IHashAlgorithm _hashAlgorithm;

        /// <summary>
        ///     Create a spelling checker large enough to handle the given number of words,
        ///     using the specified number of hashes. Implemented using a Bloom filter.
        /// </summary>
        /// <param name="hashAlgorithm"></param>
        /// <param name="wordCount"></param>
        /// <param name="hashCount"></param>
        public SpellingChecker(IHashAlgorithm hashAlgorithm, int wordCount, int hashCount)
        {
            _bits = new BitArray(ComputeBitArraySize(wordCount, hashCount));

            _hashCount = hashCount;

            _hashAlgorithm = hashAlgorithm;
        }

        private static int ComputeBitArraySize(int wordCount, int hashCount)
        {
            return wordCount * hashCount * FillFactor;
        }

        private int ComputeIndex(string word, int hashId)
        {
            unchecked
            {
                var bytes = word.SelectMany(c => Encoding.UTF8.GetBytes(new[] { char.ToLowerInvariant(c) })
                                .Concat(BitConverter.GetBytes(hashId)));

                var hash = _hashAlgorithm.ComputeHash(bytes);

                return (int)(hash % _bits.Count);
            }
        }

        /// <summary>
        /// Add a word to the dictionary.
        /// </summary>
        /// <param name="word">The word to be added.</param>
        public void Add(string word)
        {
            for (var i = 0; i < _hashCount; ++i)
            {
                _bits.Set(ComputeIndex(word, i), true);
            }
        }

        /// <summary>
        /// Check if a word is in the dictionary. There is
        /// a small possibility that a word not in the
        /// dictionary may result in a positive response
        /// if the hashed indexes collide with those
        /// of a different word or set of words in
        /// the dictionary.
        /// </summary>
        /// <param name="word">The word to check</param>
        /// <returns>False, if the word is not in the dictionary. True, if the word
        /// is in the dictionary or there was a collision with existing hashes.</returns>
        public bool Check(string word)
        {
            for (var i = 0; i < _hashCount; ++i)
            {
                var bit = ComputeIndex(word, i);
                if (!_bits[bit])
                {
                    return false;
                }
            }

            return true;
        }
    }
}