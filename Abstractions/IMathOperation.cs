namespace Abstractions
{
    using System.Collections.Generic;

    public interface IMathOperation : IOperation
    {
        char Token { get; }

        IEvaluationContext Context { get; }

        IEnumerable<IEvaluationContext> GetEvaluations(string input);

        void SetupContext(IEvaluationContext context);

        string Perform(string input);
    }
}