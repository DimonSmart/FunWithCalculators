using FunWithCalculator;
using FunWithCalculator.TwoStacksBasedCalculator;
using System;
using Xunit;

namespace FunWithCalculatorTests
{
    public class CalculatorTests
    {
        [Theory]
        [InlineData("2 + 2", "4")]
        [InlineData("2.0 + 2", "4.00")]
        [InlineData("2 + 2 * 2", "6")]
        [InlineData("(2 + 2) * 2", "8")]
        [InlineData("(2 + 2) / 2", "2")]
        [InlineData("(2 + 3) / 2", "2.50")]
        [InlineData("2 + 2 / 2", "3")]
        [InlineData("(-2)", "-2")]
        [InlineData("-2 + -2", "-4")]
        [InlineData("2 + -2", "0")]
        [InlineData("-2 + 2", "0")]
        [InlineData("-2.0 + 2", "0.00")]
        [InlineData("Pi", "3.14")]
        [InlineData("-Pi", "-3.14")]

        public void RegexBasedCalculatorTests(string expression, string expectedResult)
        {
            var calculator = new RegexBasedCalculator();
            var result = calculator.Calculate(expression);
            Assert.Equal(expectedResult, result.ToString());
        }

        [Theory]
        [InlineData("2 + 2", "4")]
        [InlineData("2.0 + 2", "4.00")]
        [InlineData("2 + 2 * 2", "6")]
        [InlineData("(2 + 2) * 2", "8")]
        [InlineData("(2 + 2) / 2", "2")]
        [InlineData("(2 + 3) / 2", "2.50")]
        [InlineData("2 + 2 / 2", "3")]
        [InlineData("(-2)", "-2")]
        [InlineData("-2 + -2", "-4")]
        [InlineData("2 + -2", "0")]
        [InlineData("-2 + 2", "0")]
        [InlineData("-2.0 + 2", "0.00")]

        public void TwoStacksBasedCalculatorTests(string expression, string expectedResult)
        {
            var calculator = new TwoStacksCalculator();
            var result = calculator.Calculate(expression);
            Assert.Equal(expectedResult, result.ToString());
        }
    }
}
