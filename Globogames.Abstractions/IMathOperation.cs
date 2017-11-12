namespace Globogames.Abstractions
{
    using System.Collections.Generic;

    public interface IMathOperation : IOperation
    {
        char Token { get; }

        IEnumerable<IEvaluationContext> GetEvaluations(string input);

        void SetupContext(IEvaluationContext context);

        IEvaluationContext Context { get; }

        string Perform(string input);

        decimal Perform(decimal[] args);

        decimal Perform(string[] args);
    }
}