namespace Calculator.Implementations.RegexCalculator.OperationTemplates
{
    using Common;

    public class SubtractionMathOperationTemplate : BaseMathOperationTemplate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubtractionMathOperationTemplate"/> class.
        /// </summary>
        public SubtractionMathOperationTemplate()
        {
            Priority = 0;
            Token = '-';
            Pattern = $"{Constants.Token}[/-]{Constants.Token}";
        }
    }
}