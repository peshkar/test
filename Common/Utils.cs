namespace Common
{
    using System;

    using Abstractions;

    public static class Utils
    {
        public static decimal Converter(string s)
        {
            s = s.Replace(".", ","); // for a simplification
            return decimal.Parse(s);
        }

        public static string ReplaceFirst(this string text, string search, string replace)
        {
            int pos = text.IndexOf(search, StringComparison.Ordinal);
            if (pos < 0)
            {
                return text;
            }

            return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
        }

        public static string ReplaceAt(this string text, string search, string replace, int index)
        {
            return text.Substring(0, index) + replace + text.Substring(index + search.Length);
        }

        public static string ReplaceAt(this IEvaluationContext context, string input, decimal replace)
        {
            return input.Substring(0, context.Index) + replace + input.Substring(context.Index + context.Token.Length);
        }

        public static string ReplaceAt(this IEvaluationContext context, string input, double replace)
        {
            return input.Substring(0, context.Index) + replace + input.Substring(context.Index + context.Token.Length);
        }

        public static bool IsHasRoundParentheses(this IEvaluationContext capture)
        {
            if (capture.Index > 0 && capture.Index + capture.Token.Length < capture.Input.Length)
            {
                var begin = capture.Input.Substring(capture.Index - 1, 1);
                var end = capture.Input.Substring(capture.Index + capture.Token.Length, 1);
                if (begin == "(" && end == ")")
                {
                    return true;
                }
            }

            return false;
        }
    }
}