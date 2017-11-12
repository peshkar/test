namespace Globogames.Calculator.Implementations.RegexCalculator
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Globogames.Abstractions;

    public class RegexOperationPicker : IOperationPicker
    {
        private readonly IMathOperationCollection _operations;

        private readonly Dictionary<char, string> _templates;

        public RegexOperationPicker(IMathOperationCollection operationCollection)
        {
            _operations = operationCollection;

            var t = Common.Constants.Token;
            _templates = new Dictionary<char, string>
                             {
                                 { '+', $"{t}[/+]{t}" },
                                 { '-', $"{t}[/-]{t}" },
                                 { '*', $"{t}[/*]{t}" },
                                 { '/', $"{t}[//]{t}" },
                                 { '^', $"{t}[/^]{t}" }
                             };
        }

        public IMathOperation Pick(string input)
        {
            IMathOperation operation;
            var context = GetEvaluationContext(input);
            if (context == null)
            {
                return null;
            }

            var chars = context.Content.ToCharArray();

            // hack for unsigned values
            if (char.IsDigit(chars[0]))
            {
                operation = _operations.FirstOrDefault(t => context.Content.Contains(t.Token));
            }
            else
            {
                var updatedValue = string.Concat(chars.Skip(1));
                operation = _operations.FirstOrDefault(t => updatedValue.Contains(t.Token));
            }

            if (operation == null)
            {
                return null;
            }

            operation.SetContext(context);
            return operation;
        }

        private static bool IsHasRoundParentheses(Capture capture, string input)
        {
            if (capture.Index > 0 && capture.Index + capture.Length < input.Length)
            {
                var begin = input.Substring(capture.Index - 1, 1);
                var end = input.Substring(capture.Index + capture.Length, 1);
                if (begin == "(" && end == ")")
                {
                    return true;
                }
            }

            return false;
        }

        private IEvaluationContext GetEvaluationContext(string input)
        {
            var result = new List<dynamic>();
            foreach (var op in _operations.OrderBy(p => p.Priority))
            {
                if (_templates.TryGetValue(op.Token, out string pattern))
                {
                    foreach (Match match in Regex.Matches(input, pattern, RegexOptions.Compiled))
                    {
                        var priority = op.Priority;

                        // increase priority of evaluation for context that has parentheses
                        if (IsHasRoundParentheses(match, input))
                        {
                            priority++;
                        }

                        result.Add(new { pattern, match, priority });
                    }
                }
            }

            var pickPattern = result.OrderByDescending(t => t.priority)
                .ThenBy(t => t.match.Index)
                .Select(t => new EvaluationContext(t.match.Index, t.match.Value))
                .FirstOrDefault();
            return pickPattern;
        }
    }
}