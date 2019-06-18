namespace Calculator.Implementations.RegexCalculator.Operations
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Abstractions;

    using Common;

    public class ExponentiationMathOperation : IMathOperation
    {
        private readonly IEvaluationContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExponentiationMathOperation"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public ExponentiationMathOperation(IEvaluationContext context)
        {
            _context = context;
        }

        public string Perform(string input)
        {
            var strings = Regex.Matches(_context.Token, Constants.Token, RegexOptions.Compiled)
                .Cast<Match>()
                .Select(m => m.Value)
                .ToArray();

            var values = Array.ConvertAll(strings, Utilities.ParseDouble);

            var result = Math.Pow(values[0], values[1]);

            input = _context.ReplaceAt(input, result);

            return input;
        }
    }
}