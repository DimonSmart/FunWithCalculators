using System;
using System.Collections.Generic;
using System.Text;
using FunWithCalculator.RegexBasedCalculator;
using FunWithCalculator.TwoStacksBasedCalculator;
using Xunit;
using Xunit.Abstractions;

namespace FunWithCalculatorTests
{
    public class MathExpressionGenerationTests
    {
        public MathExpressionGenerationTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        private readonly List<Token> _tokenGenerators = new List<Token>
        {
            () => ")",
            () => "(",
            () => Random.Next(1, 10).ToString(),
            () => "+",
            () => "-",
            () => "*"
        };

        private delegate string Token();

        private readonly ITestOutputHelper _testOutputHelper;
        private static readonly Random Random = new Random();

        private string GetRandomExpression()
        {
            var expLength = Random.Next(6, 20);
            var sb = new StringBuilder();
            for (var i = 0; i < expLength; i++)
            {
                sb.Append(_tokenGenerators[Random.Next(0, _tokenGenerators.Count)]());
                sb.Append(" ");
            }

            var expr = sb.ToString();
            return expr;
        }

        [Fact]
        // NB!!! SLOW
        // Sample output - random math expression
        // (( 8 ) ) - 2  = 6
        // 2 - 1 * 2 - 2  = 2
        // + ( 5 - 5 ) 9 - 9  = 0
        // 2 * (( 6 ) )  = 12
        // (( 5 * 7 ) )  = 35
        public void RandomExpressionEvaluator()
        {
            var samplesCount = 5;
            var c = 0;
            do
            {
                var expr = GetRandomExpression();
                try
                {
                    var calculator1 = new TwoStacksCalculator();
                    var result1 = calculator1.Calculate(expr);
                    var calculator2 = new RegexBasedCalculator();
                    var result2 = calculator2.Calculate(expr);

                    if (!result1.Equals(result2))
                    {
                        continue;
                    }

                    _testOutputHelper.WriteLine($"{expr} = {result1}");
                    c++;
                }
                catch (Exception)
                {
                    // ignored
                }
            } while (c < samplesCount);
        }
    }
}