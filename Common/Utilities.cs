namespace Common
{
    using System.Text;

    using Abstractions;

    public static class Utilities
    {
        public static bool IsHasRoundParentheses(this IEvaluationContext context)
        {
            if (context.Index > 0 && context.Index + context.Token.Length < context.Input.Length)
            {
                var begin = context.Input.Substring(context.Index - 1, 1);
                var end = context.Input.Substring(context.Index + context.Token.Length, 1);
                if (begin == "(" && end == ")")
                {
                    return true;
                }
            }

            return false;
        }

        public static decimal ParseDecimal(string input)
        {
            return decimal.Parse(input);
        }

        public static double ParseDouble(string input)
        {
            return double.Parse(input);
        }

        public static string ReplaceAt(this IEvaluationContext context, string input, decimal replace)
        {
            var sb = new StringBuilder();
            sb.Append(input.Substring(0, context.Index));
            sb.Append(replace);
            sb.Append(input.Substring(context.Index + context.Token.Length));
            return sb.ToString();
        }

        public static string ReplaceAt(this IEvaluationContext context, string input, double replace)
        {
            var sb = new StringBuilder();
            sb.Append(input.Substring(0, context.Index));
            sb.Append(replace);
            sb.Append(input.Substring(context.Index + context.Token.Length));
            return sb.ToString();
        }
    }
}