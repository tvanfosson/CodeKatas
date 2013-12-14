
using System.Linq;

namespace AnagramFinder
{
    public class HashAnagramLookup : SortAnagramLookup
    {
        private int ComputeHash(string word)
        {
            unchecked
            {
                return word.Aggregate(0, (hash, c) => hash + char.ToLowerInvariant(c));
            }
        }

        protected override bool IsAnagram(string candidate, string word)
        {
            return ComputeHash(candidate) == ComputeHash(word) && base.IsAnagram(candidate, word);
        }
    }
}
