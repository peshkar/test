namespace Calculator
{
    using System;

    using Abstractions;

    using Calculator.NinjectContext;

    using Ninject;

    class Program
    {
        static void Main(string[] args)
        {
            var regexCalculatorModule = new RegexCalculatorModule();
            var kernel = new StandardKernel(regexCalculatorModule);
            var calculator = kernel.Get<ICalculator>();

            Console.Write(
                "Ready. please, input your expression and press \"enter\". Type \"exit\" to quit."
                + Environment.NewLine);

            while (true)
            {
                try
                {
                    var input = Console.ReadLine();

                    if (string.IsNullOrEmpty(input) == false && input.Trim().ToLowerInvariant() == "exit")
                    {
                        break;
                    }

                    Console.Write(" = " + calculator.Solve(input) + Environment.NewLine);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Console.WriteLine(Environment.NewLine);
                }
            }
        }
    }
}