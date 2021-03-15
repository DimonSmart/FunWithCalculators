using System;
using System.Collections.Generic;
using FunWithCalculator.Common;
using FunWithCalculator.RegexBasedCalculator;

namespace FunWithCalculator.TwoStacksBasedCalculator
{
    public class TwoStacksCalculator : ICalculator
    {
        Stack<Number> _values;
        Stack<char> _operations;
        public Number Calculate(string expression)
        {
            _values = new Stack<Number>();
            _operations = new Stack<char>();
            var i = 0;
            while (true)
            {
                var restOfExpression = expression.Substring(i);
                if (string.IsNullOrEmpty(restOfExpression))
                {
                    break;
                }

                var number = DummyLexer.Parse(restOfExpression, out var tokenLength);
                if (number != null)
                {
                    _values.Push(number);
                    i += tokenLength;
                    continue;
                }

                if (DummyLexer.ParseExact(restOfExpression, "(", out tokenLength) != null)
                {
                    _operations.Push('(');
                    i += tokenLength;
                    continue;
                }

                if (DummyLexer.ParseExact(restOfExpression, ")", out tokenLength) != null)
                {
                    while (_operations.Peek() != '(')
                    {
                        _values.Push(Calculate(_operations.Pop(), _values.Pop(), _values.Pop()));
                    }
                    _operations.Pop();
                    i += tokenLength;
                    continue;
                }

                var operation = DummyLexer.Parse(restOfExpression, @"\+|\-|\*|\/", out tokenLength);
                if (operation != null)
                {
                    var op = operation[0];
                    while (_operations.Count > 0 && Precedence.IsPrecided(op, _operations.Peek()))
                    {
                        _values.Push(Calculate(_operations.Pop(), _values.Pop(), _values.Pop()));
                    }
                    _operations.Push(op);
                    i += tokenLength;
                }
            }

            while (_operations.Count > 0)
            {
                _values.Push(Calculate(_operations.Pop(), _values.Pop(), _values.Pop()));
            }

            return _values.Pop();
        }

        private static Number Calculate(char op, Number b, Number a)
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
