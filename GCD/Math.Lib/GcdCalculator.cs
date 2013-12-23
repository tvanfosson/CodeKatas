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

            a = System.Math.Abs(a);
            b = System.Math.Abs(b);

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
