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
        ///     using the specified number of hashes.
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

        public void Add(string word)
        {
            for (var i = 0; i < _hashCount; ++i)
            {
                _bits.Set(ComputeHash(word, i), true);
            }
        }

        public bool Check(string word)
        {
            for (var i = 0; i < _hashCount; ++i)
            {
                var bit = ComputeHash(word, i);
                if (!_bits[bit])
                {
                    return false;
                }
            }

            return true;
        }

        private static int ComputeBitArraySize(int wordCount, int hashCount)
        {
            return wordCount * hashCount * FillFactor;
        }

        private int ComputeHash(string word, int hashId)
        {
            unchecked
            {
                var bytes = word.SelectMany(c => Encoding.UTF8.GetBytes(new[] { char.ToLowerInvariant(c) })
                                .Concat(BitConverter.GetBytes(hashId)));

                var hash = _hashAlgorithm.ComputeHash(bytes);

                return (int)(hash % _bits.Count);
            }
        }
    }
}