
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnagramFinder
{
    public class HashAnagramLookup : SortAnagramLookup
    {
        private static bool AreHashEquivalent(HashedWord candidate, HashedWord word)
        {
            return candidate.GetHashCode() == word.GetHashCode();
        }

        public override IEnumerable<string> FindAnagrams(string word, IEnumerable<string> wordList)
        {
            var hashedWords = wordList.Union(new[] { word }, StringComparer.InvariantCultureIgnoreCase).Select(w => new HashedWord(w)).ToList();
            var hashedWord = new HashedWord(word);

            return hashedWords.Where(w => IsCandidate(w.Word, word.Length) 
                                            && AreHashEquivalent(w, hashedWord) 
                                            && base.IsAnagram(w.Word, word))
                              .Select(w => w.Word);
        }

    }
}
