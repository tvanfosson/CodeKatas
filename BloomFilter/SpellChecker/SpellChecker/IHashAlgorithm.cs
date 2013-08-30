using System.Collections.Generic;

namespace SpellChecker
{
    public interface IHashAlgorithm
    {
        uint ComputeHash(IEnumerable<byte> bytes);
    }
}
