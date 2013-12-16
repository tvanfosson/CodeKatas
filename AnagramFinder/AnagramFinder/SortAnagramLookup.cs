using System;
using System.Collections.Generic;
using System.Linq;

namespace AnagramFinder
{
    public class SortAnagramLookup : IAnagramLookup
    {
        private class CharCaseInsensitiveComparer : IComparer<char>
        {
            public int Compare(char x, char y)
            {
                return char.ToLower(x).CompareTo(char.ToLower(y));
            }
        }

        protected string OrderedLetters(string word)
        {
            return string.Join("", word.Where(char.IsLetter).OrderBy(c => c,new CharCaseInsensitiveComparer()));
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

            anagrams.AddRange(wordList.Where(w => string.Equals(OrderedLetters(w),orderedWord,StringComparison.OrdinalIgnoreCase)));

            return anagrams.Distinct(StringComparer.OrdinalIgnoreCase);
        }

        public virtual Dictionary<string, IEnumerable<string>> FindAnagrams(IEnumerable<string> wordList)
        {
            if (wordList == null)
            {
                throw new ArgumentNullException("wordList");
            }

            var anagrams = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);
            foreach (var word in wordList)
            {
                var key = OrderedLetters(word);
                if (anagrams.ContainsKey(key))
                {
                    anagrams[key].Add(word);
                }
                else
                {
                    anagrams.Add(key, new List<string> { word });
                }
            }

            return anagrams.Where(kv => kv.Value.Count > 1).ToDictionary(k => k.Value.First(), v => v.Value.Skip(1).AsEnumerable());
        }
    }
}
