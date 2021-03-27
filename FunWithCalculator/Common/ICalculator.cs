using FunWithCalculator.RegexBasedCalculator;
using System.Text.RegularExpressions;

namespace FunWithCalculator.Common
{
    public delegate void EvaluationStage(Match match, string before, string after);
    public interface ICalculator
    {
        Number Calculate(string expression);
        EvaluationStage OnEvaluationStage { get; set; }
    }
}
