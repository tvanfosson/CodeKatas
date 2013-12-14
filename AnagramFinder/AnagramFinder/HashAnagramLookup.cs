using System;
using System.Collections.Generic;
using System.Linq;

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

            return wordList.Union(new[] { word }, StringComparer.OrdinalIgnoreCase).Where(w => string.Equals(w, word, StringComparison.OrdinalIgnoreCase));
        }
    }
}
