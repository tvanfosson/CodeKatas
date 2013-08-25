using System;
using System.Collections;
using System.Diagnostics.Contracts;

namespace SpellChecker
{
    public class SpellingChecker : ISpellingChecker
    {
        private readonly BitArray _bits;
        private readonly int _hashCount;

        private static readonly int[] Primes = 
        {
            2, 3, 5, 7, 11, 13, 17, 19, 23, 29,
            31, 37, 41, 43, 47, 53, 59, 61, 67, 71,
            73, 79, 83, 89, 97, 101, 103, 107, 109, 113
        };

        /// <summary>
        /// Create a spelling checker large enough to handle the given number of words,
        /// using the specified number of hashes. Note: the number of hashes
        /// must be less than or equal to 30.
        /// </summary>
        /// <param name="wordCount"></param>
        /// <param name="hashCount"></param>
        public SpellingChecker(int wordCount, int hashCount)
        {
            Contract.Assert(hashCount <= Primes.Length);

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

        private int ComputeHash(string word, int hashId)
        {
            unchecked
            {
                return Math.Abs(word.GetHashCode() * Primes[hashId]) % _bits.Count;
            }
        }

        private static int ComputeBitArraySize(int wordCount, int hashCount)
        {
            var leftMostBit = (int)Math.Ceiling(Math.Log(wordCount, 2)) + 2;
            return (1 << leftMostBit) * hashCount;
        }
    }
}
