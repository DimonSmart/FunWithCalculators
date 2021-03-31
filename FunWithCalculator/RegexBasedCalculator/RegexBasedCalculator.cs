using FunWithCalculator.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace FunWithCalculator.RegexBasedCalculator
{
    public class RegexBasedCalculator : CalculatorBase
    {
        private readonly IEnumerable<RegexWithEvaluator> _evaluators
            = new List<RegexWithEvaluator>
            {
                new RegexWithEvaluator(@"(?i)\s*SIN\s*\(\s*(?<number>-?\d+\.?\d*)\s*\)\s*", Sin ,0),
                new RegexWithEvaluator(@"(?i)\s*PI\s*", Pi, 1),
                new RegexWithEvaluator(@"\s*\(\s*(?<number>-?\d+\.?\d*)\s*\)\s*", RemoveBrackets, 3),
                new RegexWithEvaluator(@"\s*(?<![\s*/])\s*(?<numberA>-?\d+\.?\d*)\s*\+\s*(?<numberB>-?\d+\.?\d*)\s*(?![\s*/])\s*", Sum, 4),
                new RegexWithEvaluator(@"\s*(?<![\s*/])\s*(?<numberA>-?\d+\.?\d*)\s*\-\s*(?<numberB>-?\d+\.?\d*)\s*(?![\s*/])\s*", Sub, 4),
                new RegexWithEvaluator(@"\s*(?<numberA>-?\d+\.?\d*)\s*\*\s*(?<numberB>-?\d+\.?\d*)\s*", Mul, 5),
                new RegexWithEvaluator(@"\s*(?<numberA>-?\d+\.?\d*)\s*\/\s*(?<numberB>-?\d+\.?\d*)\s*", Div, 5),
                new RegexWithEvaluator(@"\s*(?<numberA>-?\d+\.?\d*)\s*\+\s*(?<numberB>-?\d+\.?\d*)\s*", Sum, 6),
                new RegexWithEvaluator(@"\s*(?<numberA>-?\d+\.?\d*)\s*\+\s*(?<numberB>-?\d+\.?\d*)\s*", Sub, 6)
            };

        public override Number Calculate(string expression)
        {
            bool change;
            do
            {
                change = false;
                foreach (var priorityGroup in _evaluators.Select(i => i.PriorityGroup).Distinct())
                {
                    var matches = new List<(Match match, RegexWithEvaluator regexWithEvaluator)>();

                    foreach (var evaluator in _evaluators.Where(i=> i.PriorityGroup == priorityGroup))
                    {
                        var match = evaluator.Regex.Match(expression);
                        if (!match.Success)
                        {
                            continue;
                        }

                        matches.Add((match, evaluator));
                    }

                    if (matches.Any())
                    {
                        var leftMatch = matches.OrderBy(i => i.match.Index).First();

                        var nextStepExpression = leftMatch.regexWithEvaluator.Evaluate(leftMatch.match, expression);
                        change = true;
                        OnEvaluationStage?.Invoke(leftMatch.match, expression, nextStepExpression);
                        expression = nextStepExpression;
                        break;
                    }
                }
            } while (change);
            return Number.Create(expression);
        }

        private static string Format(double d)
        {
            return d.ToString("F2");
        }
        private static string MakeResult(Match match, string s, string result)
        {
            return s.Substring(0, match.Index) + result + s.Substring(match.Index + match.Length);
        }

        private static string RemoveBrackets(Match match, string s)
        {
            return MakeResult(match, s, match.Groups["number"].Value);
        }

        private static string Sum(Match match, string s)
        {
            var result = Number.Create(match.Groups["numberA"].Value) + Number.Create(match.Groups["numberB"].Value);
            return MakeResult(match, s, result.ToString());
        }

        private static string Sub(Match match, string s)
        {
            var result = Number.Create(match.Groups["numberA"].Value) - Number.Create(match.Groups["numberB"].Value);
            return MakeResult(match, s, result.ToString());
        }

        private static string Mul(Match match, string s)
        {
            var result = Number.Create(match.Groups["numberA"].Value) * Number.Create(match.Groups["numberB"].Value);
            return MakeResult(match, s, result.ToString());
        }

        private static string Div(Match match, string s)
        {
            var result = Number.Create(match.Groups["numberA"].Value) / Number.Create(match.Groups["numberB"].Value);
            return MakeResult(match, s, result.ToString());
        }

        private static string Sin(Match match, string s)
        {
            var result = Math.Sin(double.Parse(match.Groups["number"].Value));
            return MakeResult(match, s, Format(result));
        }

        private static string Pi(Match match, string s)
        {
            return MakeResult(match, s, Format(Math.PI));
        }
    }
}