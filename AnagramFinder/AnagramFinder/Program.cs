
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AnagramFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            var wordList = File.ReadAllLines(@"Data\wordlist.txt")
                               .Select(s => (s ?? "").Trim())
                               .Where(s => !string.IsNullOrEmpty(s))
                               .Distinct(StringComparer.InvariantCultureIgnoreCase)
                               .ToList();

            var hashAnagrams = new Dictionary<string, IEnumerable<string>>();
            var sortAnagrams = new Dictionary<string, IEnumerable<string>>();

            var hashAlgorithm = new HashAnagramLookup();
            var sortAlgorithm = new SortAnagramLookup();

            var stopwatch = Stopwatch.StartNew();

            foreach (var word in wordList)
            {
                sortAnagrams.Add(word, sortAlgorithm.FindAnagrams(word, wordList).ToList());
            }

            stopwatch.Stop();

            var sortElapsed = stopwatch.Elapsed;

            stopwatch = Stopwatch.StartNew();

            foreach (var word in wordList)
            {
                hashAnagrams.Add(word, hashAlgorithm.FindAnagrams(word, wordList).ToList());
            }

            stopwatch.Stop();

            var hashElapsed = stopwatch.Elapsed;

            Console.WriteLine("Sort algorithm Time: ", sortElapsed);
            Console.WriteLine("Hash algorithm time: ", hashElapsed);
        }
    }
}
