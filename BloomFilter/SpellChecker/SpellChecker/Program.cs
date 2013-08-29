using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;

namespace SpellChecker
{
    class Program
    {
        private struct Statistics
        {
            public int Count;
            public int Found;
        }

        static void Main(string[] args)
        {
            const string wordListPath = "wordlist.txt";
            const int hashes = 5;

            var wordCount = CountWords(wordListPath);
            var spellchecker = new SpellingChecker(wordCount, hashes);

            var fiveLetterWords = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);
            using (var reader = new StreamReader(wordListPath))
            {
                string word;
                while ((word = reader.ReadLine()) != null)
                {
                    word = word.Trim();
                    spellchecker.Add(word);
                    if (word.Length == 5)
                    {
                        fiveLetterWords.Add(word);
                    }
                }
            }

            using (var reader = new StreamReader(wordListPath))
            {
                try
                {
                    string word;
                    while ((word = reader.ReadLine()) != null)
                    {
                        Contract.Assert(spellchecker.Check(word));
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            var stats = new Statistics { Count = 50000 };
            stats.Found = GenerateWords(stats.Count, fiveLetterWords).Count(spellchecker.Check);

            Console.WriteLine("Using {0} hashes, checked {1} words, found {2} collisions", hashes, stats.Count, stats.Found);
            Console.WriteLine();
            Console.WriteLine("Completed, press the Enter key to exit.");
            Console.ReadLine();
        }

        private static IEnumerable<string> GenerateWords(int limit, ICollection<string> filterList)
        {
            for (var c1 = 'a'; c1 <= 'z'; ++c1)
            {
                for (var c2 = 'a'; c2 <= 'z'; ++c2)
                {
                    for (var c3 = 'a'; c3 <= 'z'; ++c3)
                    {
                        for (var c4 = 'a'; c4 <= 'z'; ++c4)
                        {
                            for (var c5 = '0'; c5 <= '9'; ++c5)
                            {
                                if (limit <= 0)
                                {
                                    yield break;
                                }

                                var generatedWord = string.Concat(c1, c2, c3, c4, c5);

                                if (!filterList.Contains(generatedWord))
                                {
                                    --limit;
                                    yield return generatedWord;
                                }
                            }
                        }
                    }
                }
            }
        }

        private static int CountWords(string path)
        {
            using (var reader = new StreamReader(path))
            {
                var count = 0;
                while (reader.ReadLine() != null)
                {
                    count++;
                }

                return count;
            }
        }

    }
}
