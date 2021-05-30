using NUnit.Framework;
using PayrollCalculator.Services;
using PayrollCalculator.Services.Models;
using System;
using System.Linq;
using System.Collections.Generic;

namespace PayrollCalculator.Tests
{
    [TestFixture]
    public class PayrollCalculatorServiceTest
    {
        private const double employeeBenefitCost = 1000;
        private const double dependentBenefitCost = 500;
        private const int paychecksPerYear = 26;
        
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
            
            var previewCosts = _service.CalculateEmployeeCosts(employee);

            var benefitDeduction = Math.Round(employeeBenefitCost / paychecksPerYear, 2);

            Assert.IsNotNull(previewCosts);
            Assert.AreEqual(benefitDeduction, previewCosts.CostPerPaycheck);
        }

        [Test]
        public void CalculateEmployeeCosts_EmployeeWithOneDependents()
        {
            var employee = new Employee { Dependents = new List<Dependent> { new Dependent() } };

            var previewCosts = _service.CalculateEmployeeCosts(employee);

            var benefitDeduction = Math.Round((employeeBenefitCost + dependentBenefitCost) / paychecksPerYear, 2);

            Assert.IsNotNull(previewCosts);
            Assert.AreEqual(benefitDeduction, previewCosts.CostPerPaycheck);
        }

        [Test]
        public void CalculateEmployeeCosts_EmployeeWithTwoDependents()
        {
            var employee = new Employee { Dependents = new List<Dependent> { new Dependent(), new Dependent() } };

            var previewCosts = _service.CalculateEmployeeCosts(employee);

            double dependentFinalCost = employee.Dependents.Count() * dependentBenefitCost;

            var benefitDeduction = Math.Round((employeeBenefitCost + dependentFinalCost) / paychecksPerYear, 2);

            Assert.IsNotNull(previewCosts);
            Assert.AreEqual(benefitDeduction, previewCosts.CostPerPaycheck);
        }

        [Test]
        public void CalculateEmployeeCosts_EmployeeWithNoDependents_EmployeeNameStartsWithLetterA()
        {
            var employee = new Employee { Name = "Albert" };
            var nameDiscount = .10;
            var previewCosts = _service.CalculateEmployeeCosts(employee);
            
            var benefitDeduction = Math.Round((employeeBenefitCost - (employeeBenefitCost * nameDiscount))/ paychecksPerYear, 2);

            Assert.IsNotNull(previewCosts);
            Assert.AreEqual(benefitDeduction, previewCosts.CostPerPaycheck);
        }
    }
}
