namespace Calculator.Implementations.RegexCalculator.OperationTemplates
{
    using Common;

    public class SumMathOperationTemplate : BaseMathOperationTemplate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SumMathOperationTemplate"/> class.
        /// </summary>
        public SumMathOperationTemplate()
        {
            Priority = 0;
            Token = '+';
            Pattern = $"{Constants.Token}[/+]{Constants.Token}";
        }
    }
}