using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using FunWithCalculator.Common;

namespace FunWithCalculator.RegexBasedCalculator
{
    public class RegexBasedCalculator : ICalculator
    {
        public delegate void Stage(Match match, string before, string after);

        private readonly IEnumerable<RegexWithEvaluator> _evaluators
            = new List<RegexWithEvaluator>
            {
                new RegexWithEvaluator(@"(?i)\s*SIN\s*\(\s*(?<number>-?\d+\.?\d*)\s*\)\s*", Sin),
                new RegexWithEvaluator(@"\s*\(\s*(?<number>-?\d*\.?\d*)\s*\)\s*", RemoveBrackets),
                new RegexWithEvaluator(@"\s*(?<numberA>-?\d+\.?\d*)\s*\*\s*(?<numberB>-?\d+\.?\d*)\s*", Mul),
                new RegexWithEvaluator(@"\s*(?<numberA>-?\d+\.?\d*)\s*\/\s*(?<numberB>-?\d+\.?\d*)\s*", Div),
                new RegexWithEvaluator(@"(?i)\s*PI\s*", Pi),
                new RegexWithEvaluator(@"\s*(?<numberA>-?\d+\.?\d*)\s*\+\s*(?<numberB>-?\d+\.?\d*)\s*", Sum)
            };

        public Stage Step;

        public Number Calculate(string expression)
        {
            bool change;
            do
            {
                change = false;
                foreach (var evaluator in _evaluators)
                {
                    var match = evaluator.Regex.Match(expression);
                    if (!match.Success)
                    {
                        continue;
                    }

                    var nextStepExpression = evaluator.Evaluate(match, expression);
                    change = true;
                    Step?.Invoke(match, expression, nextStepExpression);
                    expression = nextStepExpression;
                    break;
                }
            } while (change);

            return Number.Create(expression);
        }

        private static string Format(double d)
        {
            return d.ToString("F2");
        }

        private static string RemoveBrackets(Match match, string s)
        {
            return Regex.Replace(s, Regex.Escape(match.Value), match.Groups["number"].Value);
        }

        private static string Sum(Match match, string s)
        {
            var result = Number.Create(match.Groups["numberA"].Value) + Number.Create(match.Groups["numberB"].Value);
            return Regex.Replace(s, Regex.Escape(match.Value), result.ToString());
        }

        private static string Mul(Match match, string s)
        {
            var result = Number.Create(match.Groups["numberA"].Value) * Number.Create(match.Groups["numberB"].Value);
            return Regex.Replace(s, Regex.Escape(match.Value), result.ToString());
        }

        private static string Div(Match match, string s)
        {
            var result = Number.Create(match.Groups["numberA"].Value) / Number.Create(match.Groups["numberB"].Value);
            return Regex.Replace(s, Regex.Escape(match.Value), result.ToString());
        }

        private static string Sin(Match match, string s)
        {
            var result = Math.Sin(double.Parse(match.Groups["number"].Value));
            return Regex.Replace(s, Regex.Escape(match.Value), Format(result));
        }

        private static string Pi(Match match, string s)
        {
            return Regex.Replace(s, Regex.Escape(match.Value), Format(Math.PI));
        }
    }
}