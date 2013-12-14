using System;
using System.Collections.Generic;
using System.Linq;

namespace AnagramFinder
{
    public class HashAnagramLookup : IAnagramLookup
    {
        private static bool IsAnagram(string candidate, string word)
        {
            var reverse = string.Join("", candidate.Reverse());
            var isAnagram = string.Equals(word, candidate, StringComparison.OrdinalIgnoreCase) || string.Equals(word, reverse, StringComparison.OrdinalIgnoreCase);
            return isAnagram;
        }

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

            return wordList.Union(new[] { word }, StringComparer.OrdinalIgnoreCase).Where(w => IsAnagram(w, word));
        }
    }
}
