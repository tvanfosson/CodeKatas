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

            throw new NotImplementedException();
        }
    }
}
