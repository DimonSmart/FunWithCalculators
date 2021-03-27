using FunWithCalculator.RegexBasedCalculator;

namespace FunWithCalculator.Common
{
    public abstract class CalculatorBase : ICalculator
    {
        public abstract Number Calculate(string expression);
        public EvaluationStage OnEvaluationStage { get; set; }
    }
}