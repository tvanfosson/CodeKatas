using System;
using System.Collections.Generic;
using System.Linq;

namespace AnagramFinder
{
    public class HashAnagramLookup : IAnagramLookup
    {
        // ordered by frequency of occurrence in English to help minimize the size of the key value and prevent overflow for very long words
        // http://en.wikipedia.org/wiki/Letter_frequency#Relative_frequencies_of_letters_in_the_English_language
        private static readonly Dictionary<char, int> PrimeDictionary = new Dictionary<char, int>
        {
            {'e', 2}, {'t', 3}, {'a', 5}, {'o', 7}, {'i', 11}, {'n', 13}, {'s', 17}, {'h', 19}, {'r', 23}, {'d', 29},
            {'l', 31}, {'c', 37}, {'u', 41}, {'m', 43}, {'w', 47}, {'f', 53}, {'g', 59}, {'y', 61}, {'p', 67}, {'b', 71},
            {'v', 73}, {'k', 79}, {'j', 83}, {'x', 89}, {'q', 97}, {'z', 101}
        };

        private static long ComputeHash(string word)
        {
            return word.Aggregate(1L, (h, c) =>
            {
                if (char.IsLetter(c))
                {
                    h = h * PrimeDictionary[char.ToLowerInvariant(c)];
                }
                return h;
            });
        }

        public IEnumerable<string> FindAnagrams(string word, IEnumerable<string> wordList)
        {
            var hash = ComputeHash(word);

            var anagrams = new List<string> { word };

            anagrams.AddRange(wordList.Where(w => ComputeHash(w) == hash));

            return anagrams.Distinct(StringComparer.OrdinalIgnoreCase);
        }

        public Dictionary<string, IEnumerable<string>> FindAnagrams(IEnumerable<string> wordList)
        {
            if (wordList == null)
            {
                throw new ArgumentNullException("wordList");
            }

            var anagrams = new Dictionary<long, List<string>>();
            foreach (var word in wordList)
            {
                 var key = ComputeHash(word);
                if (anagrams.ContainsKey(key))
                {
                    anagrams[key].Add(word);
                }
                else
                {
                    anagrams.Add(key, new List<string> { word });
                }
            }

            return anagrams.Where(kv => kv.Value.Count > 1).ToDictionary(kv => kv.Value.First(), kv => kv.Value.Skip(1).AsEnumerable());
        }
    }
}
