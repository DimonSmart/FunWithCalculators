using FunWithCalculator.RegexBasedCalculator;
using FunWithCalculator.TwoStacksBasedCalculator;
using System.Text.RegularExpressions;
using Xunit;
using Xunit.Abstractions;

namespace FunWithCalculatorTests
{
    public class CalculatorTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public CalculatorTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Theory]
        [InlineData("4* 2 + 2", "10")]
        [InlineData("8 3 + - ( 9 )", "2")]
        [InlineData("(Sin(Pi))+1", "1.00")]
        [InlineData("2 + 2", "4")]
        [InlineData("2.0 + 2", "4.00")]
        [InlineData("2 + 2 * 2", "6")]
        [InlineData("(2 + 2) * 2", "8")]
        [InlineData("(2 + 2) / 2", "2")]
        [InlineData("(2 + 3) / 2", "2.50")]
        [InlineData("2 + 2 / 2", "3")]
        [InlineData("(0-2)", "-2")]
        [InlineData("0-2 + 0-2", "-4")]
        [InlineData("2 + (0-2)", "0")]
        [InlineData("0-2 + 2", "0")]
        [InlineData("0-2.0 + 2", "0.00")]
        [InlineData("Pi", "3.14")]
        [InlineData("-Pi", "-3.14")]
        [InlineData("2 + 2 * (2 + 2)", "10")]
        [InlineData("7-9+2", "0")]
        [InlineData("7+9-2", "14")]
        [InlineData("7-9+10", "8")]
        public void RegexBasedCalculatorTests(string expression, string expectedResult)
        {
            var calculator = new RegexBasedCalculator();
            calculator.OnEvaluationStage += PrintStep;
            Assert.Equal(expectedResult, calculator.Calculate(expression).ToString());
        }

        private void PrintStep(Match match, string before, string after)
        {
            _testOutputHelper.WriteLine($"{before} => {after}; Match: {match.Value}");
        }

        [Theory]
        [InlineData("4* 2 + 2", "10")]
        [InlineData("8 3 + - ( 9 )", "2")]
        // [InlineData("(Sin(Pi))+1", "1.00")]
        [InlineData("2 + 2", "4")]
        [InlineData("2.0 + 2", "4.00")]
        [InlineData("2 + 2 * 2", "6")]
        [InlineData("(2 + 2) * 2", "8")]
        [InlineData("(2 + 2) / 2", "2")]
        [InlineData("(2 + 3) / 2", "2.50")]
        [InlineData("2 + 2 / 2", "3")]
        [InlineData("(0-2)", "-2")]
        [InlineData("0-2 + 0-2", "-4")]
        [InlineData("2 + (0-2)", "0")]
        [InlineData("0-2 + 2", "0")]
        [InlineData("0-2.0 + 2", "0.00")]
        // [InlineData("Pi", "3.14")]
        // [InlineData("-Pi", "-3.14")]
        [InlineData("2 + 2 * (2 + 2)", "10")]
        [InlineData("7-9+2", "0")]
        [InlineData("7+9-2", "14")]
        [InlineData("7-9+10", "8")]
        public void TwoStacksBasedCalculatorTests(string expression, string expectedResult)
        {
            var calculator = new TwoStacksCalculator();
            calculator.OnEvaluationStage += PrintStep;
            Assert.Equal(expectedResult, calculator.Calculate(expression).ToString());
        }
    }
}