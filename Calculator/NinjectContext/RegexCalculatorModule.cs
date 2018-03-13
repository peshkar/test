namespace Calculator.NinjectContext
{
    using System;
    using System.Collections.Generic;

    using Abstractions;

    using Calculator.Implementations.RegexCalculator;
    using Calculator.Implementations.RegexCalculator.Collections;
    using Calculator.Implementations.RegexCalculator.Operations;
    using Calculator.Implementations.RegexCalculator.OperationTemplates;

    using Ninject.Modules;

    public class RegexCalculatorModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ICalculator>().To<RegexCalculator>().InSingletonScope();
            Bind<IOperationPicker>().To<RegexOperationPicker>().InSingletonScope();
            IDictionary<IMathOperationTemplate, Type> obj = new Dictionary<IMathOperationTemplate, Type>
            {
                { new SumMathOperationTemplate(), typeof(SumMathOperation) },
                { new SubtractionMathOperationTemplate(), typeof(SubtractionMathOperation) },
                { new MultiplicationMathOperationTemplate(), typeof(MultiplicationMathOperation) },
                { new DivisionMathOperationTemplate(), typeof(DivisionMathOperation) },
                { new ExponentiationMathOperationTemplate(), typeof(ExponentiationMathOperation) }
            };

            Bind<IDictionary<IMathOperationTemplate, Type>>().ToConstant(obj);
            Bind<ITransformOperationCollection>().To<TransformOperationCollection>().InSingletonScope();
        }
    }
}