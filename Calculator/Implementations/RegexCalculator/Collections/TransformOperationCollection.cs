namespace Calculator.Implementations.RegexCalculator.Collections
{
    using System.Collections;
    using System.Collections.Generic;

    using Abstractions;

    using Calculator.Implementations.RegexCalculator.Transform;

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