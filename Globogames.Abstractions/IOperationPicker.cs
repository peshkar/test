namespace Globogames.Abstractions
{
    public interface IOperationPicker
    {
        IMathOperation Pick(string input);
    }
}