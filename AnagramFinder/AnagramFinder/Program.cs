
using System;
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
                               .Distinct(StringComparer.CurrentCultureIgnoreCase)
                               .ToList();

            var sortAlgorithm = new SortAnagramLookup();

            var stopwatch = Stopwatch.StartNew();

            var sortAnagrams = sortAlgorithm.FindAnagrams(wordList);

            stopwatch.Stop();

            var sortElapsed = stopwatch.Elapsed;

            Console.WriteLine("Sort algorithm found {0} anagrams", sortAnagrams.Count);

            Console.WriteLine("Sort algorithm Time: {0}", sortElapsed);


            var hashAlgorithm = new HashAnagramLookup();

             stopwatch = Stopwatch.StartNew();

            var hashAnagrams = hashAlgorithm.FindAnagrams(wordList);

            stopwatch.Stop();

            var hashElapsed = stopwatch.Elapsed;

            Console.WriteLine("Hash algorithm found {0} anagrams", hashAnagrams.Count);

            Console.WriteLine("Hash algorithm Time: {0}", hashElapsed);

#if DEBUG
            Console.ReadKey();
#endif
        }
    }
}
