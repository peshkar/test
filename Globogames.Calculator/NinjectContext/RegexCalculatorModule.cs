namespace Globogames.Calculator.NinjectContext
{
    using Globogames.Abstractions;
    using Globogames.Calculator.Implementations.RegexCalculator;
    using Globogames.Calculator.Implementations.RegexCalculator.Operations;

    using Ninject.Modules;

    public class RegexCalculatorModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ICalculator>().To<RegexCalculator>().InSingletonScope();
            Bind<IOperationPicker>().To<RegexOperationPicker>().InSingletonScope();
            Bind<IMathOperationCollection>().To<MathOperationsCollection>().InSingletonScope();
            Bind<ITransformOperationCollection>().To<TransformOperationCollection>().InSingletonScope();
        }
    }
}