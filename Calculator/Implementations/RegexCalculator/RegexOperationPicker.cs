namespace Calculator.Implementations.RegexCalculator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Abstractions;

    using Common;

    public class RegexOperationPicker : IOperationPicker
    {
        private readonly IDictionary<IMathOperationTemplate, Type> _operationsMappings;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegexOperationPicker"/> class.
        /// </summary>
        /// <param name="operationMappings">
        /// The operation collection.
        /// </param>
        public RegexOperationPicker(IDictionary<IMathOperationTemplate, Type> operationMappings)
        {
            _operationsMappings = operationMappings;
        }

        public IMathOperation Pick(string input)
        {
            var list = new List<dynamic>();
            var templates = _operationsMappings.Select(t => t.Key);
            foreach (var operation in templates.OrderByDescending(p => p.Priority))
            {
                foreach (var context in operation.GetEvaluationsContexts(input))
                {
                    context.SetPriority(operation.Priority);

                    // increase priority of evaluation for context that has parentheses
                    if (context.IsHasRoundParentheses())
                    {
                        context.SetPriority(context.Priority + 1);
                    }

                    list.Add(new { context, operation });
                }
            }

            var nextStep = list.OrderByDescending(t => t.context.Priority)
                .ThenBy(t => t.context.Index)
                .Select(
                    t => new
                             {
                                 context = (IEvaluationContext)t.context,
                                 operation = (IMathOperationTemplate)t.operation
                             })
                .FirstOrDefault(t => t.context.Token.Contains(t.operation.Token));

            if (nextStep != null)
            {
                if (_operationsMappings.TryGetValue(nextStep.operation, out Type type))
                {
                    return (IMathOperation)Activator.CreateInstance(type, nextStep.context);
                }
            }

            return null;
        }
    }
}