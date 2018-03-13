namespace Abstractions
{
    using System.Collections.Generic;

    public interface IMathOperationTemplate : IOperation
    {
        char Token { get; }

        IEnumerable<IEvaluationContext> GetEvaluationsContexts(string input);
    }
}