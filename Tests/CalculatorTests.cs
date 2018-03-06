namespace Tests
{
    using Abstractions;

    using Calculator.NinjectContext;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Ninject;

    [TestClass]
    public class UnitTest1
    {
        private readonly IKernel _kernel;

        public UnitTest1()
        {
            var regexCalculatorModule = new RegexCalculatorModule();
            _kernel = new StandardKernel(regexCalculatorModule);
        }

        [TestMethod]
        public void TestMethod()
        {
            var calculator = _kernel.Get<ICalculator>();
            Assert.AreEqual(
                -2462,
                calculator.Solve("2*2*3+(5+5)-(7*8+(4-5+5)*3+(4*3)+(3+3+3+3)*(4*(4*(23+3))))/2+50+2"));
            Assert.AreEqual(15, calculator.Solve("3*5"));
            Assert.AreEqual(86.2m, calculator.Solve("23 * 2 + 45 - 24 / 5"));
            Assert.AreEqual(925m, calculator.Solve("555/3*5"));
            Assert.AreEqual(342.9375m, calculator.Solve("2*2*3+(5+5)-(7*8+(4-5^5)*3+(4*3)+(3+3+3+3)*(4*(4*(23+3))))/2^4+50+2"));
        }
    }
}