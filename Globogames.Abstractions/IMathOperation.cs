namespace Globogames.Abstractions
{
    public interface IMathOperation : IOperation
    {
        char Token { get; }

        void SetContext(IEvaluationContext context);

        string Perform(string input);

        decimal Perform(decimal[] args);

        decimal Perform(string[] args);
    }
}