using System;
using System.Collections.Generic;
using System.Linq;

namespace AnagramFinder
{
    public class SortAnagramLookup : IAnagramLookup
    {
        protected virtual bool IsAnagram(string candidate, string word)
        {
            if (word.Length != candidate.Length)
            {
                return false;
            }

            var candidateLetters = candidate.ToCharArray().OrderBy(c => c).ToArray();
            var wordLetters = word.ToCharArray().OrderBy(c => c);

            return !wordLetters.Where((t, i) => char.ToLowerInvariant(candidateLetters[i]) != char.ToLowerInvariant(t)).Any();
        }

        public virtual IEnumerable<string> FindAnagrams(string word, IEnumerable<string> wordList)
        {
            if (string.IsNullOrEmpty(word))
            {
                throw new ArgumentNullException("word");
            }

            if (wordList == null)
            {
                throw new ArgumentNullException("wordList");
            }

            return wordList.Union(new[] { word }, StringComparer.InvariantCultureIgnoreCase).Where(w => IsAnagram(w, word));
        }
    }
}
