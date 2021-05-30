using PayrollCalculator.Services.Models;
using System;
using System.Collections.Generic;
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
                    CostPerPaycheck = GetEmployeeBenefitCost(employee)
                };
            }

            var employeeFinalCost = NameStartsWithLetterA(employee.Name)
                ? employeeBenefitCostPerYear - (employeeBenefitCostPerYear * nameBenefitDiscount)
                : employeeBenefitCostPerYear;

            double dependentFinalCost = GetDependentsFinalCost(employee.Dependents);

            return new CostPreview
            {
                CostPerPaycheck = CalculateCost(employeeFinalCost + dependentFinalCost)
            };
        }

        private double GetDependentsFinalCost(IEnumerable<Dependent> dependents)
        {
            var dependentFinalCost = 0.0;

            foreach (var dependent in dependents)
            {
                dependentFinalCost += NameStartsWithLetterA(dependent.Name)
                    ? (dependentBenefitCostPerYear - (dependentBenefitCostPerYear * .10))
                    : dependentBenefitCostPerYear;
            }

            return dependentFinalCost;
        }

        private double GetEmployeeBenefitCost(Employee employee)
        {
            return NameStartsWithLetterA(employee.Name)
                ? CalculateCost(employeeBenefitCostPerYear - (employeeBenefitCostPerYear * nameBenefitDiscount))
                : CalculateCost(employeeBenefitCostPerYear);
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
