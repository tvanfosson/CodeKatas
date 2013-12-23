using System;

namespace Math.Lib
{
    public class GcdCalculator : IGcdCalculator
    {
        public int GreatestCommonDenominator(int a, int b)
        {
            if (a == 0 && b == 0)
            {
                throw new ArgumentOutOfRangeException("a");
            }

            while (b != 0)
            {
                var temp = b;
                b = a % b;
                a = temp;
            }

            return a >= 0 ? a : -a;
        }
    }
}
