namespace FunWithCalculator
{
    public class Number
    {
        public bool IsInteger { get; set; }
        public double DoubleValue { get; set; }
        public int IntValue { get; set; }

        public double AsDouble()
        {
            if (IsInteger)
            {
                return IntValue;
            }

            return DoubleValue;
        }

        public static Number Create(string s)
        {
            if (s.Contains("."))
            {
                return new Number
                {
                    DoubleValue = double.Parse(s),
                    IsInteger = false
                };
            }

            return new Number
            {
                IntValue = int.Parse(s),
                IsInteger = true
            };
        }

        internal static Number FromValue(int value)
        {
            return new Number
            {
                IntValue = value,
                IsInteger = true
            };
        }

        internal static Number FromValue(double value)
        {
            return new Number
            {
                DoubleValue = value,
                IsInteger = false
            };
        }

        public static Number operator +(Number a, Number b)
        {
            if (a.IsInteger && b.IsInteger)
            {
                return FromValue(a.IntValue + b.IntValue);
            }
            return FromValue(a.AsDouble() + b.AsDouble());
        }

        public static Number operator -(Number a, Number b)
        {
            if (a.IsInteger && b.IsInteger)
            {
                return FromValue(a.IntValue - b.IntValue);
            }
            return FromValue(a.AsDouble() - b.AsDouble());
        }

        public static Number operator *(Number a, Number b)
        {
            if (a.IsInteger && b.IsInteger)
            {
                return FromValue(a.IntValue * b.IntValue);
            }
            return FromValue(a.AsDouble() * b.AsDouble());
        }

        public static Number operator /(Number a, Number b)
        {
            if (a.IsInteger && b.IsInteger && a.IntValue == a.IntValue / b.IntValue * b.IntValue)
            {
                return FromValue(a.IntValue / b.IntValue);
            }
            return FromValue(a.AsDouble() / b.AsDouble());
        }

        public override string ToString()
        {
            if (IsInteger)
            {
                return IntValue.ToString();
            }
            return DoubleValue.ToString("N2");
        }
    }
}
