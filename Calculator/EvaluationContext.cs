namespace Calculator
{
    using Abstractions;

    public class EvaluationContext : IEvaluationContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EvaluationContext"/> class.
        /// </summary>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <param name="token">
        /// The content.
        /// </param>
        /// <param name="input">
        /// The input.
        /// </param>
        public EvaluationContext(int index, string token, string input)
        {
            Index = index;
            Token = token;
            Input = input;
        }

        public int Index { get; }

        public string Token { get; }

        public int Priority { get; private set; }

        public string Input { get; }

        public void SetPriority(int newPriority)
        {
            Priority = newPriority;
        }
    }
}