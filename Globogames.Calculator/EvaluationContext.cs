namespace Globogames.Calculator
{
    using Globogames.Abstractions;

    public class EvaluationContext : IEvaluationContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EvaluationContext"/> class.
        /// </summary>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <param name="content">
        /// The content.
        /// </param>
        public EvaluationContext(int index, string content)
        {
            Index = index;
            Content = content;
        }

        public int Index { get; }

        public string Content { get; }
    }
}