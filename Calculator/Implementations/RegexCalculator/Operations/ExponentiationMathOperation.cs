﻿namespace Calculator.Implementations.RegexCalculator.Operations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Abstractions;

    using Common;

    public class ExponentiationMathOperation : IMathOperation
    {
        private readonly string _pattern;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExponentiationMathOperation"/> class.
        /// </summary>
        /// <param name="token">
        /// The token.
        /// </param>
        /// <param name="priority">
        /// The priority.
        /// </param>
        public ExponentiationMathOperation(char token, int priority)
        {
            Priority = priority;
            Token = token;
            _pattern = $"{Constants.Token}[/^]{Constants.Token}";
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
            var strings = Regex.Matches(Context.Content, Constants.Token, RegexOptions.Compiled)
                .Cast<Match>()
                .Select(m => m.Value)
                .ToArray();

            var decimals = Array.ConvertAll(strings, Utils.Converter);

            var result = Math.Pow((double)decimals[0], (double)decimals[1]);

            input = Context.ReplaceAt(input, result);

            return input;
        }
    }
}