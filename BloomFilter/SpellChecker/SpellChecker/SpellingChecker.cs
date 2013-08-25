using System;
using System.Collections;

namespace SpellChecker
{
    public class SpellingChecker : ISpellingChecker
    {
        private readonly BitArray _bits;
        private readonly int _hashCount;

        public SpellingChecker(int wordCount, int hashCount)
        {
            _bits = new BitArray(ComputeBitArraySize(wordCount, hashCount));

            _hashCount = hashCount;
        }

        public void Add(string word)
        {
            throw new NotImplementedException();
        }

        public bool Check(string word)
        {
            throw new NotImplementedException();
        }

        private int ComputeHash(string word, int hashId)
        {
            throw new NotImplementedException();
        }

        private static int ComputeBitArraySize(int wordCount, int hashCount)
        {
            var leftMostBit = (int)Math.Ceiling(Math.Log(wordCount, 2)) + 2;
            return (1 << leftMostBit) * hashCount;
        }
    }
}
