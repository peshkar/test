namespace Tests
{
    using Abstractions;

    using Calculator.NinjectContext;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Ninject;

    [TestClass]
    public class RegexCalculatorTests
    {
        private ICalculator _calculator;

        [TestInitialize]
        public void Setup()
        {
            var _kernel = new StandardKernel(new RegexCalculatorModule());
            _calculator = _kernel.Get<ICalculator>();
        }

        [TestMethod]
        [DataRow("-2462", "2*2*3+(5+5)-(7*8+(4-5+5)*3+(4*3)+(3+3+3+3)*(4*(4*(23+3))))/2+50+2")]
        [DataRow("15", "3*5")]
        [DataRow("86.2", "23 * 2 + 45 - 24 / 5")]
        [DataRow("925", "555/3*5")]
        [DataRow("342.9375", "2*2*3+(5+5)-(7*8+(4-5^5)*3+(4*3)+(3+3+3+3)*(4*(4*(23+3))))/2^4+50+2")]
        public void Solve_ShouldReturn_ExpectedResult(string expectedInput, string input)
        {
            var expected = decimal.Parse(expectedInput);
            Assert.AreEqual(expected, _calculator.Solve(input));
        }
    }
}