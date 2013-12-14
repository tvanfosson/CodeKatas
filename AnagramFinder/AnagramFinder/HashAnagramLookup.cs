using System;
using System.Collections.Generic;

namespace AnagramFinder
{
    public class HashAnagramLookup : IAnagramLookup
    {
        public IEnumerable<string> FindAnagrams(string word, IEnumerable<string> wordList)
        {
            if (string.IsNullOrEmpty(word))
            {
                throw new ArgumentNullException("word");
            }

            if (wordList == null)
            {
                throw new ArgumentNullException("wordList");
            }

            throw new NotImplementedException();
        }
    }
}
