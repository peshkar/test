namespace Calculator.Implementations.RegexCalculator.OperationTemplates
{
    using Common;

    public class MultiplicationMathOperationTemplate : BaseMathOperationTemplate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MultiplicationMathOperationTemplate"/> class.
        /// </summary>
        public MultiplicationMathOperationTemplate()
        {
            Priority = 1;
            Token = '*';
            Pattern = $"{Constants.Token}[*]{Constants.Token}";
        }
    }
}