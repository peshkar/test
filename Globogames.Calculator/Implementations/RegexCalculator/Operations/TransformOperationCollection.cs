namespace Globogames.Calculator.Implementations.RegexCalculator.Operations
{
    using System.Collections;
    using System.Collections.Generic;

    using Globogames.Abstractions;

    public class TransformOperationCollection : ITransformOperationCollection
    {
        public TransformOperationCollection()
        {
            TransformOperations = new List<ITransformOperation> { new OpenParenthesesTransformOperation(0) };
        }

        public IEnumerable<ITransformOperation> TransformOperations { get; }

        public IEnumerator<ITransformOperation> GetEnumerator()
        {
            return TransformOperations.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return TransformOperations.GetEnumerator();
        }
    }
}