namespace Globogames.Calculator.Implementations.RegexCalculator
{
    using Globogames.Abstractions;

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