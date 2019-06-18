namespace Calculator.Implementations.RegexCalculator.Operations
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Abstractions;

    using Common;

    public class DivisionMathOperation : IMathOperation
    {
        private readonly IEvaluationContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="DivisionMathOperation"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public DivisionMathOperation(IEvaluationContext context)
        {
            _context = context;
        }

        public string Perform(string input)
        {
            var strings = Regex.Matches(_context.Token, Constants.Token, RegexOptions.Compiled)
                .Cast<Match>()
                .Select(m => m.Value)
                .ToArray();

            var decimals = Array.ConvertAll(strings, Utilities.ParseDecimal);

            var result = decimals[0] / decimals[1];

            input = _context.ReplaceAt(input, result);

            return input;
        }
    }
}