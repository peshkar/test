namespace Globogames.Calculator.Implementations.RegexCalculator.Operations
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Globogames.Abstractions;
    using Globogames.Common;

    public class SubtractionMathOperation : IMathOperation
    {
        private readonly string _pattern;

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
            _pattern = $"{Constants.Token}[/-]{Constants.Token}";
        }

        public char Token { get; }

        public int Priority { get; set; }

        public IEvaluationContext Context { get; private set; }

        public IEnumerable<IEvaluationContext> GetEvaluations(string input)
        {
            return Regex.Matches(input, _pattern, RegexOptions.Compiled)
                .Cast<Match>()
                .Select(m => new EvaluationContext(m.Index, m.Value))
                .ToArray();
        }

        public void SetupContext(IEvaluationContext context)
        {
            Context = context;
        }

        public string Perform(string input)
        {
            var strings = Regex.Matches(Context.Content, Constants.Token).Cast<Match>().Select(m => m.Value).ToArray();
            var result = Perform(strings);
            input = input.ReplaceAt(Context.Content, result.ToString(CultureInfo.InvariantCulture), Context.Index);
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