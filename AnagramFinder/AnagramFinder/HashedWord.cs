using System.Linq;

namespace AnagramFinder
{
    public class HashedWord
    {
        public HashedWord(string word)
        {
            Word = word;
            _hashCode = ComputeHash();
        }

        public string Word { get; private set; }

        private int ComputeHash()
        {
            unchecked
            {
                return Word.Aggregate(0, (hash, c) => hash + char.ToLowerInvariant(c));
            }
        }


        private readonly int _hashCode;
        public override int GetHashCode()
        {
            return _hashCode;
        }
    }
}
