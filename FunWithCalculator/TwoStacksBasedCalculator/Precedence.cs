using System.Collections.Generic;

namespace FunWithCalculator.TwoStacksBasedCalculator
{
    public static class Precedence
    {
        private static readonly IDictionary<char, int> OperationPriority = new Dictionary<char, int>
            {
               { '*', 3 },
               { '/', 3 },
               { '+', 2 },
               { '-', 2 },
               { '(', 1 },
               { ')', 1 }
            };

        public static bool IsPrecided(char operationA, char operationB)
        {
            return OperationPriority[operationA] <= OperationPriority[operationB];
        }
    }
}
