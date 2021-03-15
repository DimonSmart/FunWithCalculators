using System.Text.RegularExpressions;

namespace FunWithCalculator.RegexBasedCalculator
{
    public class RegexWithEvaluator
    {
        public delegate string Evaluator(Match match, string s);

        public RegexWithEvaluator(string pattern, Evaluator calculator)
        {
            Regex = new Regex(pattern, RegexOptions.Compiled);
            Evaluate = calculator;
        }

        public Regex Regex { get; set; }
        public Evaluator Evaluate { get; set; }
    }
}
