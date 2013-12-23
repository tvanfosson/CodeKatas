using System;
using GCD.MathLib;

namespace GCD
{
    class Program
    {
        static void Main(string[] args)
        {
#if DEBUG
            args = new[] { "17", "3" };
#endif
            if (args.Length != 2)
            {
                Usage();
            }

            try
            {
                var calculator = new GcdCalculator();

                var gcd = calculator.GreatestCommonDenominator(int.Parse(args[0]), int.Parse(args[1]));

                Console.WriteLine("gcd({0},{1}) = {2}", args[0], args[1], gcd);
            }
            catch (FormatException)
            {
                Usage();
            }
#if DEBUG
            Console.ReadKey();
#endif
        }

        private static void Usage()
        {
            Console.WriteLine("usage: GCD a b");
            Console.WriteLine("where a and b are integers");
            Environment.Exit(0);
        }
    }
}
