using NUnit.Framework;
using PayrollCalculator.Services;

namespace PayrollCalculator.Tests
{
    [TestFixture]
    public class PayrollCalculatorServiceTest
    {
        private PayrollCalculatorService _service;

        [SetUp]
        public void SetUp()
        {
            _service = new PayrollCalculatorService();
        }
    }
}
