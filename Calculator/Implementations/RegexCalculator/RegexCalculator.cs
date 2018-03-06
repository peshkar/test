namespace Calculator.Implementations.RegexCalculator
{
    using Abstractions;

    public class RegexCalculator : BaseCalculator
    {
        public RegexCalculator(
            IOperationPicker operationPicker,
            ITransformOperationCollection transformOperationCollection)
            : base(operationPicker, transformOperationCollection)
        {
        }
    }
}