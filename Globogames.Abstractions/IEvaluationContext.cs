namespace Globogames.Abstractions
{
    public interface IEvaluationContext
    {
        int Index { get; }

        string Content { get; }

        int Priority { get; set; }
    }
}