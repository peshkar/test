namespace Calculator.Implementations.RegexCalculator.OperationTemplates
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Abstractions;

    using Common;

    public class DivisionMathOperationTemplate : IMathOperationTemplate
    {
        private readonly string _pattern;

        public DivisionMathOperationTemplate()
        {
            Priority = 1;
            Token = '/';
            _pattern = $"{Constants.Token}[//]{Constants.Token}";
        }

        public char Token { get; }

        public int Priority { get; set; }

        public IEnumerable<IEvaluationContext> GetEvaluationsContexts(string input)
        {
            return Regex.Matches(input, _pattern, RegexOptions.Compiled)
                .Cast<Match>()
                .Select(m => new EvaluationContext(m.Index, m.Value, input))
                .ToArray();
        }
    }
}