namespace Globogames.Calculator.Implementations.RegexCalculator
{
    using System.Collections.Generic;
    using System.Linq;

    using Globogames.Abstractions;

    public class RegexOperationPicker : IOperationPicker
    {
        private readonly IMathOperationCollection _operations;

        public RegexOperationPicker(IMathOperationCollection operationCollection)
        {
            _operations = operationCollection;
        }

        public IMathOperation Pick(string input)
        {
            var list = new List<dynamic>();
            foreach (var op in _operations.OrderByDescending(p => p.Priority))
            {
                foreach (var context in op.GetEvaluations(input))
                {
                    context.Priority = op.Priority;

                    // increase priority of evaluation for context that has parentheses
                    if (IsHasRoundParentheses(context, input))
                    {
                        context.Priority++;
                    }

                    list.Add(new { context, operation = op });
                }
            }

            var nextStep = list.OrderByDescending(t => t.context.Priority)
                .ThenBy(t => t.context.Index)
                .FirstOrDefault();

            // solve the operation with the context
            if (nextStep != null)
            {
                IMathOperation resultOperation = nextStep.operation;

                resultOperation.SetupContext(nextStep.context);

                return resultOperation;
            }

            return null;
        }

        private static bool IsHasRoundParentheses(IEvaluationContext capture, string input)
        {
            if (capture.Index > 0 && capture.Index + capture.Content.Length < input.Length)
            {
                var begin = input.Substring(capture.Index - 1, 1);
                var end = input.Substring(capture.Index + capture.Content.Length, 1);
                if (begin == "(" && end == ")")
                {
                    return true;
                }
            }

            return false;
        }
    }
}