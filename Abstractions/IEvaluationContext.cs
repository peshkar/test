namespace Abstractions
{
    public interface IEvaluationContext
    {
        int Index { get; }

        string Token { get; }

        int Priority { get; }

        string Input { get; }

        void SetPriority(int newPriority);
    }
}