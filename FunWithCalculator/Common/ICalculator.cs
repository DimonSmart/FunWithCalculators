using FunWithCalculator.RegexBasedCalculator;

namespace FunWithCalculator.Common
{
    public interface ICalculator
    {
        Number Calculate(string expression);
    }
}
