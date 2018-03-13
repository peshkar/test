namespace Calculator.Implementations.RegexCalculator.OperationTemplates
{
    using Common;

    public class ExponentiationMathOperationTemplate : BaseMathOperationTemplate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExponentiationMathOperationTemplate"/> class.
        /// </summary>
        public ExponentiationMathOperationTemplate()
        {
            Priority = 2;
            Token = '^';
            Pattern = $"{Constants.Token}[/^]{Constants.Token}";
        }
    }
}