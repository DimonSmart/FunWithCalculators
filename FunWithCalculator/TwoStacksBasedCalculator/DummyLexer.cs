using System.Text.RegularExpressions;
using FunWithCalculator.RegexBasedCalculator;

namespace FunWithCalculator.TwoStacksBasedCalculator
{
    public static class DummyLexer
    {
        public static Number Parse(string s, out int length)
        {
            var value = Parse(s, @"[-+]?[0-9]+(?:\.[0-9]+)?", out length);
            return value == null ? null : Number.Create(value);
        }

        public static string ParseExact(string s, string target, out int length)
        {
            return Parse(s, Regex.Escape(target), out length);
        }

        public static string Parse(string s, string target, out int length)
        {
            Regex regex = new Regex($@"^\s*(?<value>{target})\s*");
            var result = regex.Match(s);
            if (!result.Success)
            {
                length = 0;
                return null;
            }
            length = result.Length;
            return result.Groups["value"].Value;
        }
    }
}
