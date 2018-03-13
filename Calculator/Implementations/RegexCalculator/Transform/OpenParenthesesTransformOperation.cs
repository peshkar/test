namespace Calculator.Implementations.RegexCalculator.Transform
{
    using System.Text.RegularExpressions;

    using Abstractions;

    using Common;

    public class OpenParenthesesTransformOperation : ITransformOperation
    {
        private readonly string ParenthesesToken;

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenParenthesesTransformOperation"/> class.
        /// </summary>
        /// <param name="priority">
        /// The priority.
        /// </param>
        public OpenParenthesesTransformOperation(int priority)
        {
            Priority = priority;
            ParenthesesToken = $"[(]{Constants.Token}[)]";
        }

        public int Priority { get; set; }

        public string Transform(string input)
        {
            var matches = Regex.Matches(input, ParenthesesToken, RegexOptions.Compiled);
            foreach (Match match in matches)
            {
                var leftReplaceResult = match.Value.Replace("(", string.Empty);
                var finalResult = leftReplaceResult.Replace(")", string.Empty);
                input = Regex.Replace(input, ParenthesesToken, finalResult);
                input = Transform(input);
            }

            return input;
        }
    }
}