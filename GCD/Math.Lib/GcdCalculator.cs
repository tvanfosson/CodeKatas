using System;
using System.Threading;

namespace Math.Lib
{
    public class GcdCalculator : IGcdCalculator
    {

        public int GreatestCommonDenominator(int a, int b)
        {
            if (a == b)
            {
                return a;
            }
            else if (a - b > 0)
            {
                return GreatestCommonDenominator(a - b, b);
            }
            else
            {
                return GreatestCommonDenominator(b - a, a);
            }
        }
    }
}
