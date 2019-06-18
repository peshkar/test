namespace Calculator
{
    using System;
    using System.Linq;

    using Abstractions;

    using Common;

    public abstract class BaseCalculator : ICalculator
    {
        private readonly IOperationPicker _operationPicker;

        private readonly ITransformOperationCollection _transformOperationCollection;

        protected BaseCalculator(IOperationPicker operationPicker, ITransformOperationCollection transformOperationCollection)
        {
            _operationPicker = operationPicker;
            _transformOperationCollection = transformOperationCollection;
        }

        public virtual decimal Solve(string input)
        {
            // clean spaces
            input = input.Replace(" ", string.Empty);

            while (true)
            {
                // step 1 - transformation
                foreach (var transformOperation in _transformOperationCollection.OrderByDescending(t => t.Priority))
                {
                    input = transformOperation.Transform(input);
                }

                // step 2 - get next operation
                var operation = _operationPicker.Pick(input);
                if (operation == null)
                {
                    break;
                }

                // step 3 - perform this operation
                input = operation.Perform(input);

                Console.WriteLine(input);
            }

            return Utilities.ParseDecimal(input);
        }
    }
}