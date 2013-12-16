using System.Collections.Generic;

namespace AnagramFinder
{
    public interface IAnagramLookup
    {
        /// <summary>
        /// Given a word and a list of words, find all the words in the list
        /// that are anagrams of the given word.
        /// </summary>
        /// <param name="word">The starting word.</param>
        /// <param name="wordList">A list of candidate words.</param>
        /// <returns>A list of words from the word list, including the start word, that are anagrams of the start word.</returns>
        IEnumerable<string> FindAnagrams(string word, IEnumerable<string> wordList);

        /// <summary>
        /// Given a list of words, find all the words that have anagrams in the list and thsoe anagrams
        /// that are anagrams of the given word.
        /// </summary>
        /// <param name="wordList">A list of candidate words.</param>
        /// <returns>A list of words from the word list, including the start word, that are anagrams of the start word.</returns>
        Dictionary<string,IEnumerable<string>> FindAnagrams(IEnumerable<string> wordList);
    }
}
