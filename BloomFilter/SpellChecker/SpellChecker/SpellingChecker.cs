using System.Collections;
using System.Diagnostics.Contracts;
using System.Linq;

namespace SpellChecker
{
    public class SpellingChecker : ISpellingChecker
    {
        private const int FillFactor = 16;
        private const int InitialPrime = 17;

        private static readonly int[] Primes =
        {
            31, 37, 41, 43, 47, 53, 59, 61, 67, 71,
            73, 79, 83, 89, 97, 101, 103, 107, 109, 113,
            127, 131, 137, 139, 149, 151, 157, 163, 167, 173
        };

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
            Contract.Assert(hashCount < Primes.Length);

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
            return wordCount*hashCount*FillFactor;
        }

        private int ComputeHash(string word, int hashId)
        {
            unchecked
            {
                var hash = word.Aggregate(InitialPrime,
                    (current, c) => current*Primes[hashId] + char.ToLowerInvariant(c).GetHashCode());

                return (int) ((uint) hash%_bits.Count);
            }
        }
    }
}