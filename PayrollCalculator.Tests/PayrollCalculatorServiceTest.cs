using NUnit.Framework;
using PayrollCalculator.Services;
using PayrollCalculator.Services.Models;
using System;
using System.Collections.Generic;

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

        [Test]
        public void CalculateEmployeeCosts_EmployeeWithNoDependents()
        {
            var employee = new Employee();
            decimal benefitCost = 1000;
            int yearlyPaychecks = 26;

            var previewCosts = _service.CalculateEmployeeCosts(employee);

            var benefitDeduction = Math.Round(benefitCost / yearlyPaychecks, 2);

            Assert.IsNotNull(previewCosts);
            Assert.AreEqual(benefitDeduction, previewCosts.CostPerPaycheck);
        }

        [Test]
        public void CalculateEmployeeCosts_EmployeeWithOneDependent()
        {
            var employee = new Employee { Dependents = new List<Dependent> { new Dependent() } };

            decimal benefitCost = 1000;
            decimal dependentCost = 500;
            int yearlyPaychecks = 26;

            var previewCosts = _service.CalculateEmployeeCosts(employee);

            var benefitDeduction = Math.Round((benefitCost + dependentCost) / yearlyPaychecks, 2);

            Assert.IsNotNull(previewCosts);
            Assert.AreEqual(benefitDeduction, previewCosts.CostPerPaycheck);
        }
    }
}
