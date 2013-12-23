using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Math.Lib.Test
{
    [TestClass]
    public class GcdCalculatorTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void When_both_numbers_are_zero_an_exception_is_thrown()
        {
            var calculator = new GcdCalculator();
            calculator.GreatestCommonDenominator(0, 0);
        }

        [TestMethod]
        public void When_the_numbers_are_the_same_the_GCD_value_is_the_number_itself()
        {
            const int expected = 53;
            var calculator = new GcdCalculator();
            var actual = calculator.GreatestCommonDenominator(expected, expected);

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void When_the_first_number_is_a_multiple_of_the_second_the_second_is_returned()
        {
            const int expected = 7;
            var calculator = new GcdCalculator();
            var actual = calculator.GreatestCommonDenominator(expected * 3, expected);

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void When_the_second_number_is_a_multiple_of_the_first_the_first_is_returned()
        {
            const int expected = 7;
            var calculator = new GcdCalculator();
            var actual = calculator.GreatestCommonDenominator(expected, expected * 3);

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void When_the_numbers_share_no_factors_other_than_one_one_is_returned()
        {
            const int expected = 1;
            const int a = 3;
            const int b = 7;
            var calculator = new GcdCalculator();
            var actual = calculator.GreatestCommonDenominator(a, b);

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void When_the_numbers_have_a_GCD_greater_than_one_it_is_returned()
        {
            const int expected = 17;
            const int a = 3 * expected;
            const int b = 7 * expected;
            var calculator = new GcdCalculator();
            var actual = calculator.GreatestCommonDenominator(a, b);

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void When_the_numbers_have_multiple_common_factors_the_largest_is_returned()
        {
            const int expected = 17 * 7 * 3 * 2;
            const int a = 3 * expected;
            const int b = 7 * expected;
            var calculator = new GcdCalculator();
            var actual = calculator.GreatestCommonDenominator(a, b);

            Assert.AreEqual(actual, expected);
        }
    }
}
