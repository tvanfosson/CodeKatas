using System.Collections.Generic;

namespace SpellChecker
{
    // ReSharper disable once InconsistentNaming
    public class FNV1aHashAlgorithm : IHashAlgorithm
    {
        private const uint FnvBasis = 2166136261U;
        private const uint FnvPrime = 16777619U;

        public uint ComputeHash(IEnumerable<byte> bytes)
        {
            unchecked
            {
                var hash = FnvBasis;
                foreach (var c in bytes)
                {
                    hash = hash ^ c;
                    hash = hash * FnvPrime;
                }

                return hash;
            }
        }
    }
}
