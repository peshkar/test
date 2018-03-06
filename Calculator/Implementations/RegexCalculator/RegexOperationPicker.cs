namespace Calculator.Implementations.RegexCalculator
{
    using System.Collections.Generic;
    using System.Linq;

    using Abstractions;

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
            foreach (var operation in _operations.OrderByDescending(p => p.Priority))
            {
                foreach (var context in operation.GetEvaluations(input))
                {
                    context.Priority = operation.Priority;

                    // increase priority of evaluation for context that has parentheses
                    if (IsHasRoundParentheses(context, input))
                    {
                        context.Priority++;
                    }

                    list.Add(new { context, operation });
                }
            }

            var nextStep = list.OrderByDescending(t => t.context.Priority)
                .ThenBy(t => t.context.Index)
                .Select(t => 
                    new
                        {
                            context = (IEvaluationContext)t.context,
                            operation = (IMathOperation)t.operation
                        })
                .FirstOrDefault(t => t.context.Content.Contains(t.operation.Token));

            // solve the operation with the context
            if (nextStep != null)
            {
                var resultOperation = nextStep.operation;

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