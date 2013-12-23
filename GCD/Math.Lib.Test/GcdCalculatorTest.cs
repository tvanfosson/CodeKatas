using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Math.Lib.Test
{
    [TestClass]
    public class GcdCalculatorTest
    {
        [TestMethod]
        public void When_the_numbers_are_the_same_the_GCD_value_is_the_number_itself()
        {
            const int expected = 53;
            var calculator = new GcdCalculator();
            var actual = calculator.GreatestCommonDenominator(expected, expected);

            Assert.AreEqual(actual, expected);
        }
    }
}
