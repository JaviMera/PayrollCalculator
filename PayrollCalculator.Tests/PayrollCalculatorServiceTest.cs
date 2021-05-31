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
            var employeeCost = Math.Round(employeeBenefitCost / paychecksPerYear, 2);
            var totalCost = employeeCost;

            var previewCosts = _service.CalculateEmployeeCosts(employee);

            Assert.IsNotNull(previewCosts);
            Assert.AreEqual(employeeCost, previewCosts.EmployeeCost);
            Assert.AreEqual(totalCost, previewCosts.TotalCost);
        }

        [Test]
        public void CalculateEmployeeCosts_EmployeeWithOneDependents()
        {
            var employee = new Employee { Dependents = new List<Dependent> { new Dependent() } };
            var employeeCost = Math.Round(employeeBenefitCost / paychecksPerYear, 2);
            var dependentCost = Math.Round(dependentBenefitCost / paychecksPerYear, 2);
            var totalCost = employeeCost + dependentCost;

            var previewCosts = _service.CalculateEmployeeCosts(employee);

            Assert.IsNotNull(previewCosts);
            Assert.AreEqual(employeeCost, previewCosts.EmployeeCost);
            Assert.AreEqual(dependentCost, previewCosts.DependentCost);
            Assert.AreEqual(totalCost, previewCosts.TotalCost);
        }

        [Test]
        public void CalculateEmployeeCosts_EmployeeWithTwoDependents()
        {
            var employee = new Employee { Dependents = new List<Dependent> { new Dependent(), new Dependent() } };

            double dependentFinalCost = employee.Dependents.Count() * dependentBenefitCost;

            var employeeCost = Math.Round(employeeBenefitCost / paychecksPerYear, 2);
            var dependentCost = Math.Round(dependentFinalCost / paychecksPerYear, 2);
            var totalCost = employeeCost + dependentCost;

            var previewCosts = _service.CalculateEmployeeCosts(employee);

            Assert.IsNotNull(previewCosts);
            Assert.AreEqual(employeeCost, previewCosts.EmployeeCost);
            Assert.AreEqual(dependentCost, previewCosts.DependentCost);
            Assert.AreEqual(totalCost, previewCosts.TotalCost);
        }

        [Test]
        public void CalculateEmployeeCosts_EmployeeWithNoDependents_EmployeeNameStartsWithLetterA()
        {
            var employee = new Employee { Name = "Albert" };
            var nameDiscount = .10;

            var employeeCost = Math.Round((employeeBenefitCost - (employeeBenefitCost * nameDiscount)) / paychecksPerYear, 2);
            var totalCost = employeeCost;

            var previewCosts = _service.CalculateEmployeeCosts(employee);
            
            Assert.IsNotNull(previewCosts);
            Assert.AreEqual(employeeCost, previewCosts.EmployeeCost);
            Assert.AreEqual(totalCost, previewCosts.TotalCost);
        }

        [Test]
        public void CalculateEmployeeCosts_EmployeeWithOneDependent_DependentNameStartsWithLetterA()
        {
            var employee = new Employee { Dependents = new List<Dependent> { new Dependent { Name = "Astrid" } } };
            var nameDiscount = .10;

            var employeeCost = Math.Round(employeeBenefitCost / paychecksPerYear, 2);            
            var dependentCost = Math.Round((dependentBenefitCost - (dependentBenefitCost * nameDiscount)) / paychecksPerYear, 2);
            var totalCost = employeeCost + dependentCost;
            var previewCosts = _service.CalculateEmployeeCosts(employee);

            Assert.IsNotNull(previewCosts);
            Assert.AreEqual(employeeCost, previewCosts.EmployeeCost);
            Assert.AreEqual(dependentCost, previewCosts.DependentCost);
            Assert.AreEqual(totalCost, previewCosts.TotalCost);
        }

        [Test]
        public void CalculateEmployeeCosts_EmployeeWithOneDependent_EmployeeAndDependentNameStartsWithLetterA()
        {
            var employee = new Employee { Name = "Albert", Dependents = new List<Dependent> { new Dependent { Name = "Astrid" } } };
            var nameDiscount = .10;

            var employeeDeduction = employeeBenefitCost - (employeeBenefitCost * nameDiscount);            
            var dependentDeduction = (dependentBenefitCost - (dependentBenefitCost * nameDiscount));

            var employeeCost = Math.Round(employeeDeduction / paychecksPerYear, 2);
            var dependentCost = Math.Round(dependentDeduction / paychecksPerYear, 2);
            var totalCost = employeeCost + dependentCost;

            var previewCosts = _service.CalculateEmployeeCosts(employee);

            Assert.IsNotNull(previewCosts);
            Assert.AreEqual(employeeCost, previewCosts.EmployeeCost);
            Assert.AreEqual(dependentCost, previewCosts.DependentCost);
            Assert.AreEqual(totalCost, previewCosts.TotalCost);
        }        

        [Test]
        public void CalculateEmployeeCosts_EmployeeWithTwoDependents_EmployeeAndOneDependentNameStartsWithLetterA()
        {
            var employee = new Employee
            {
                Name = "Albert",
                Dependents = new List<Dependent> {
                    new Dependent { Name = "Astrid" },
                    new Dependent { Name = "Charles" }
                }
            };

            var nameDiscount = .10;

            var employeeCost = Math.Round((employeeBenefitCost - (employeeBenefitCost * nameDiscount)) / paychecksPerYear, 2);
            var dependentCost = Math.Round(dependentBenefitCost / paychecksPerYear, 2) + Math.Round((dependentBenefitCost - (dependentBenefitCost * nameDiscount)) / paychecksPerYear, 2);
            var totalCost = employeeCost + dependentCost;

            var previewCosts = _service.CalculateEmployeeCosts(employee);

            Assert.IsNotNull(previewCosts);
            Assert.AreEqual(employeeCost, previewCosts.EmployeeCost);
            Assert.AreEqual(dependentCost, previewCosts.DependentCost);
            Assert.AreEqual(totalCost, previewCosts.TotalCost);
        }

        [Test]
        public void CalculateEmployeeCosts_EmployeeWithTwoDependents_OnlyDependentsNameStartsWithLetterA()
        {
            var employee = new Employee
            {
                Name = "Mario",
                Dependents = new List<Dependent> {
                    new Dependent { Name = "Astrid" },
                    new Dependent { Name = "Alicia" }
                }
            };

            var nameDiscount = .10;

            var employeeCost = Math.Round(employeeBenefitCost / paychecksPerYear, 2); ;
            var dependentCost = employee.Dependents.Count() * Math.Round((dependentBenefitCost - (dependentBenefitCost * nameDiscount)) / paychecksPerYear, 2);
            var totalCost = employeeCost + dependentCost;

            var previewCosts = _service.CalculateEmployeeCosts(employee);

            Assert.IsNotNull(previewCosts);
            Assert.AreEqual(employeeCost, previewCosts.EmployeeCost);
            Assert.AreEqual(dependentCost, previewCosts.DependentCost);
            Assert.AreEqual(totalCost, previewCosts.TotalCost);
        }
    }
}
