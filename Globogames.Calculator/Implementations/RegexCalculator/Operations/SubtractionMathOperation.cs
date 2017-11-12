namespace Globogames.Calculator.Implementations.RegexCalculator.Operations
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Globogames.Abstractions;
    using Globogames.Common;

    public class SubtractionMathOperation : IMathOperation
    {
        private IEvaluationContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="SubtractionMathOperation"/> class.
        /// </summary>
        /// <param name="token">
        /// The token.
        /// </param>
        /// <param name="priority">
        /// The priority.
        /// </param>
        public SubtractionMathOperation(char token, int priority)
        {
            Priority = priority;
            Token = token;
        }

        public char Token { get; }

        public int Priority { get; }

        public void SetContext(IEvaluationContext context)
        {
            _context = context;
        }

        public string Perform(string input)
        {
            var strings = Regex.Matches(_context.Content, Constants.Token)
                .Cast<Match>()
                .Select(m => m.Value)
                .ToArray();
            var result = Perform(strings);
            input = input.ReplaceAt(_context.Content, result.ToString(CultureInfo.InvariantCulture), _context.Index);
            return input;
        }

        public decimal Perform(string[] args)
        {
            return Perform(Array.ConvertAll(args, Utils.Converter));
        }

        public decimal Perform(decimal[] args)
        {
            // a little hack
            return args[0] - Math.Abs(args[1]);
        }
    }
}