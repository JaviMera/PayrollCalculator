using PayrollCalculator.Services.Models;
using System;
using System.Linq;

namespace PayrollCalculator.Services
{
    public sealed class PayrollCalculatorService : IPayrollCalculatorService
    {
        private const int paychecksPerYear = 26;
        private const double employeeBenefitCostPerYear = 1000;
        private const double dependentBenefitCostPerYear = 500;
        private const double nameBenefitDiscount = .10;

        public CostPreview CalculateEmployeeCosts(Employee employee)
        {
            if (!employee.Dependents.Any())
            {

                return new CostPreview
                {
                    CostPerPaycheck = NameStartsWithLetterA(employee.Name)
                        ? CalculateCost(employeeBenefitCostPerYear - (employeeBenefitCostPerYear * nameBenefitDiscount))
                        : CalculateCost(employeeBenefitCostPerYear)
                };
            }

            var employeeFinalCost = NameStartsWithLetterA(employee.Name)
                ? employeeBenefitCostPerYear - (employeeBenefitCostPerYear * nameBenefitDiscount)
                : employeeBenefitCostPerYear;

            var dependentFinalCost = 0.0;

            foreach(var dependent in employee.Dependents)
            {
                dependentFinalCost += NameStartsWithLetterA(dependent.Name)
                    ? (dependentBenefitCostPerYear - (dependentBenefitCostPerYear * .10))
                    : dependentBenefitCostPerYear;
            }

            return new CostPreview
            {
                CostPerPaycheck = CalculateCost(employeeFinalCost + dependentFinalCost)
            };
        }

        private static bool NameStartsWithLetterA(string name)
        {
            return name.StartsWith('A');
        }

        private double CalculateCost(double cost)
        {
            return Math.Round(cost / paychecksPerYear, 2);
        }
    }
}
