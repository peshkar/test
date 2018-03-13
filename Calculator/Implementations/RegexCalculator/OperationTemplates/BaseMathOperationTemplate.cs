namespace Calculator.Implementations.RegexCalculator.OperationTemplates
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Abstractions;

    public class BaseMathOperationTemplate : IMathOperationTemplate
    {
        public string Pattern { get; set; }

        public char Token { get; set; }

        public int Priority { get; set; }

        public IEnumerable<IEvaluationContext> GetEvaluationsContexts(string input)
        {
            return Regex.Matches(input, Pattern, RegexOptions.Compiled)
                .Cast<Match>()
                .Select(m => new EvaluationContext(m.Index, m.Value, input))
                .ToList();
        }
    }
}