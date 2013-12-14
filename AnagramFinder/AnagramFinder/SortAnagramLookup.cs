using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnagramFinder
{
    public class SortAnagramLookup : IAnagramLookup
    {
        protected bool IsCandidate(string candidate, int length)
        {
            return candidate.Length == length;
        }

        protected string OrderedLetters(string word)
        {
            return string.Join("", word.OrderBy(c => c));
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

            var anagrams = new List<string> { word };
            var orderedWord = OrderedLetters(word);

            anagrams.AddRange(wordList.Where(w => IsCandidate(w, word.Length) && string.Equals(OrderedLetters(w),orderedWord,StringComparison.CurrentCultureIgnoreCase)));

            return anagrams.Distinct(StringComparer.OrdinalIgnoreCase);
        }

        public virtual Dictionary<string, IEnumerable<string>> FindAnagrams(IEnumerable<string> wordList)
        {
            var anagrams = new Dictionary<string, List<string>>(StringComparer.CurrentCultureIgnoreCase);
            foreach (var word in wordList)
            {
                var key = OrderedLetters(word);
                if (anagrams.ContainsKey(key))
                {
                    anagrams[key].Add(word);
                }
                else
                {
                    anagrams.Add(key, new List<string>());
                }
            }

            return anagrams.Where(kv => kv.Value.Any()).ToDictionary(k => k.Key, v => v.Value.AsEnumerable());
        }
    }
}
