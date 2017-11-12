namespace Globogames.Calculator.Implementations.RegexCalculator
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Globogames.Abstractions;

    public class RegexOperationPicker : IOperationPicker
    {
        private readonly IMathOperationCollection _mathOperations;

        private readonly Dictionary<string, string> _templates;

        public RegexOperationPicker(IMathOperationCollection mathOperationCollection)
        {
            _mathOperations = mathOperationCollection;
            _templates = new Dictionary<string, string>
                             {
                                 {
                                     "+",
                                     $"{Common.Constants.Token}[/+]{Common.Constants.Token}"
                                 },
                                 {
                                     "-",
                                     $"{Common.Constants.Token}[/-]{Common.Constants.Token}"
                                 },
                                 {
                                     "*",
                                     $"{Common.Constants.Token}[/*]{Common.Constants.Token}"
                                 },
                                 {
                                     "/",
                                     $"{Common.Constants.Token}[//]{Common.Constants.Token}"
                                 },
                                 {
                                     "^",
                                     $"{Common.Constants.Token}[/^]{Common.Constants.Token}"
                                 }
                             };
        }

        public IMathOperation Pick(string input)
        {
            IMathOperation mathOperation;

            var context = GetEvaluationContext(input);

            if (context == null)
            {
                return null;
            }

            var chars = context.Content.ToCharArray();

            // hack for unsigned values
            if (char.IsDigit(chars[0]))
            {
                mathOperation = _mathOperations.FirstOrDefault(t => context.Content.Contains(t.Token));
                if (mathOperation != null)
                {
                    mathOperation.SetContext(context);
                    return mathOperation;
                }
            }
            else
            {
                var updatedValue = string.Concat(chars.Skip(1));
                mathOperation = _mathOperations.FirstOrDefault(t => updatedValue.Contains(t.Token));
                if (mathOperation != null)
                {
                    mathOperation.SetContext(context);
                    return mathOperation;
                }
            }

            return null;
        }

        private IEvaluationContext GetEvaluationContext(string input)
        {
            var result = new List<dynamic>();
            foreach (var operation in _mathOperations)
            {
                if (_templates.TryGetValue(operation.Token.ToString(), out string pattern))
                {
                    foreach (Match match in Regex.Matches(input, pattern, RegexOptions.Compiled))
                    {
                        var priority = operation.Priority;

                        // increase priority of evaluation has parentheses
                        if (match.Index > 0 && match.Index + match.Length < input.Length)
                        {
                            var begin = input.Substring(match.Index - 1, 1);
                            var end = input.Substring(match.Index + match.Length, 1);
                            if (begin == "(" && end == ")")
                            {
                                priority++;
                            }
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