
namespace AnagramFinder
{
    public class HashAnagramLookup : SortAnagramLookup
    {
        protected override bool IsAnagram(string candidate, string word)
        {
            return base.IsAnagram(candidate, word);
        }
    }
}
