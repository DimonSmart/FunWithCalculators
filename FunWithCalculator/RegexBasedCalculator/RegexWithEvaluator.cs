using System.Text.RegularExpressions;

namespace FunWithCalculator.RegexBasedCalculator
{
    public class RegexWithEvaluator
    {
        public delegate string Evaluator(Match match, string s);

        public RegexWithEvaluator(string pattern, Evaluator calculator, int priorityGroup)
        {
            Regex = new Regex(pattern, RegexOptions.Compiled);
            Evaluate = calculator;
            PriorityGroup = priorityGroup;
        }

        public int PriorityGroup  { get; set; }
        public Regex Regex { get; set; }
        public Evaluator Evaluate { get; set; }
    }
}
