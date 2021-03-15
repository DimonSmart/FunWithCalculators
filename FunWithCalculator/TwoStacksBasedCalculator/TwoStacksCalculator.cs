using System;
using System.Collections.Generic;

namespace FunWithCalculator.TwoStacksBasedCalculator
{
    public class TwoStacksCalculator : ICalculator
    {
        Stack<Number> values;
        Stack<char> ops;
        public Number Calculate(string expression)
        {
            values = new Stack<Number>();
            ops = new Stack<char>();
            int i = 0;
            while(true)
            {
                var restOfExpression = expression.Substring(i);
                if (string.IsNullOrEmpty(restOfExpression))
                {
                    break;
                }
                int tokenLength;
                var number = DummyLexer.Parse(restOfExpression, out tokenLength);
                if (number != null)
                {
                    values.Push(number);
                    i += tokenLength;
                    continue;
                }

                if (DummyLexer.ParseExact(restOfExpression, "(", out tokenLength) != null)
                {
                    ops.Push('(');
                    i += tokenLength;
                    continue;
                }

                if (DummyLexer.ParseExact(restOfExpression, ")", out tokenLength) != null)
                {
                    while (ops.Peek() != '(')
                    {
                        values.Push(Calculate(ops.Pop(), values.Pop(), values.Pop()));
                    }
                    ops.Pop();
                    i += tokenLength;
                    continue;
                }

                var operation = DummyLexer.Parse(restOfExpression, @"\+|\-|\*|\/", out tokenLength);
                if (operation != null)
                {
                    var op = operation[0];
                    while (ops.Count > 0 && Precedence.IsPrecided(op, ops.Peek()))
                    {
                        values.Push(Calculate(ops.Pop(), values.Pop(), values.Pop()));
                    }
                    ops.Push(op);
                    i += tokenLength;
                    continue;
                }
            }

            while (ops.Count > 0)
            {
                values.Push(Calculate(ops.Pop(), values.Pop(), values.Pop()));
            }

            return values.Pop();
        }
        public static Number Calculate(char op, Number b, Number a)
        {
            switch (op)
            {
                case '+':
                    return a + b;
                case '-':
                    return a - b;
                case '*':
                    return a * b;
                case '/':
                    return a / b;
                default:
                    throw new InvalidOperationException(op.ToString());
            }
        }
    }
}
