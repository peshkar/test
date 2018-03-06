namespace Abstractions
{
    public interface ITransformOperation : IOperation
    {
        string Transform(string input);
    }
}