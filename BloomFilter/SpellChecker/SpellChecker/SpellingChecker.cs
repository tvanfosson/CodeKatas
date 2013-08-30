using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpellChecker
{
    public class SpellingChecker : ISpellingChecker
    {
        private const int FillFactor = 16;

        private readonly BitArray _bits;
        private readonly int _hashCount;

        /// <summary>
        ///     Create a spelling checker large enough to handle the given number of words,
        ///     using the specified number of hashes.
        /// </summary>
        /// <param name="wordCount"></param>
        /// <param name="hashCount"></param>
        public SpellingChecker(int wordCount, int hashCount)
        {
            _bits = new BitArray(ComputeBitArraySize(wordCount, hashCount));

            _hashCount = hashCount;
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

        private static uint ComputeHash(IEnumerable<byte> bytes)
        {
            const uint fnvBasis = 2166136261U;
            const uint fnvPrime = 16777619U;

            var hash = fnvBasis;
            foreach (var c in bytes)
            {
                hash = hash ^ c;
                hash = hash * fnvPrime;
            }

            return hash;
        }

        private int ComputeHash(string word, int hashId)
        {
            unchecked
            {
                var bytes = word.SelectMany(c => Encoding.UTF8.GetBytes(new[] { char.ToLowerInvariant(c) })
                                .Concat(BitConverter.GetBytes(hashId)));

                var hash = ComputeHash(bytes);

                return (int)(hash % _bits.Count);
            }
        }
    }
}