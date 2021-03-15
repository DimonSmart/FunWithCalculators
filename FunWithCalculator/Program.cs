using System;
using System.Text.RegularExpressions;

namespace FunWithCalculator
{
    static class Program
    {
        static void Main(string[] args)
        {
            var calculator = new RegexBasedCalculator.RegexBasedCalculator
            {
                Step = PrintStep
            };
            var result = calculator.Calculate(args[0]);
            Console.WriteLine($"={result}");
        }

        private static void PrintStep(Match match, string before, string after)
        {
            Console.WriteLine($"{before} => {after}");
        }
    }
}
