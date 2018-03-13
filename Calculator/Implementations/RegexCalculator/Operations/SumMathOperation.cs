﻿namespace Calculator.Implementations.RegexCalculator.Operations
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Abstractions;

    using Calculator.Implementations.RegexCalculator.OperationTemplates;

    using Common;

    public class SumMathOperation : SumMathOperationTemplate, IMathOperation
    {
        private readonly IEvaluationContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="SumMathOperation"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public SumMathOperation(IEvaluationContext context)
        {
            _context = context;
        }

        public string Perform(string input)
        {
            var strings = Regex.Matches(_context.Token, Constants.Token, RegexOptions.Compiled)
                .Cast<Match>()
                .Select(m => m.Value)
                .ToArray();

            var decimals = Array.ConvertAll(strings, Utils.Converter);

            var result = decimals[0] + decimals[1];

            input = _context.ReplaceAt(input, result);

            return input;
        }
    }
}