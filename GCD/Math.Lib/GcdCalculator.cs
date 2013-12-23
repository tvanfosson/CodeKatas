using System;
using System.Threading;

namespace Math.Lib
{
    public class GcdCalculator : IGcdCalculator
    {

        public int GreatestCommonDenominator(int a, int b)
        {
            while (a != b)
            {
                if (a > b)
                {
                    a = a - b;
                }
                else
                {
                    var temp = b;
                    b = a;
                    a = temp - a;
                }
            }

            return b;
        }
    }
}
