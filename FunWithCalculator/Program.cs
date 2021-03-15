using System;
using System.Text.RegularExpressions;

namespace FunWithCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            var calculator = new RegexBasedCalculator
            {
                Step = PrintStep
            };
            var result = calculator.Calculate("2 + 2 / 2");
            Console.WriteLine($"={result}");
        }

        private static void PrintStep(Match match, string before, string after)
        {
            Console.WriteLine($"{before} => {after}");
        }
    }
}
