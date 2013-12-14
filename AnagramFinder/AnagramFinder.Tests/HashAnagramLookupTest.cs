using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AnagramFinder.Tests
{
    [TestClass]
    public class HashAnagramLookupTest
    {
        private HashAnagramLookupTestContext _c;

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void When_a_null_or_empty_word_is_given_an_argument_null_exception_is_thrown()
        {
            var algorithm = _c.GetAlgorithm();

            algorithm.FindAnagrams(null, new[] { "word" });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void When_a_null_word_list_is_given_an_argument_null_exception_is_thrown()
        {
            var algorithm = _c.GetAlgorithm();

            algorithm.FindAnagrams("word", null);
        }


        [TestInitialize]
        public void Init()
        {
            _c = new HashAnagramLookupTestContext();
        }

        private class HashAnagramLookupTestContext
        {
            public IAnagramLookup GetAlgorithm()
            {
                return new HashAnagramLookup();
            }
        }
    }
}
