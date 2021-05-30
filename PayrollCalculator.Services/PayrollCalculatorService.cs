﻿using PayrollCalculator.Services.Models;
using System;
using System.Linq;

namespace PayrollCalculator.Services
{
    public sealed class PayrollCalculatorService : IPayrollCalculatorService
    {
        private const int paychecksPerYear = 26;
        private const double benefitCostPerYear = 1000;
        private const double dependentCostPerYear = 500;
        private const double benefitDiscount = .10;

        public CostPreview CalculateEmployeeCosts(Employee employee)
        {
            if (!employee.Dependents.Any())
            {

                return new CostPreview
                {
                    CostPerPaycheck = employee.Name.StartsWith('A')
                        ? Math.Round((benefitCostPerYear - (benefitCostPerYear * benefitDiscount)) / paychecksPerYear, 2)
                        : Math.Round(benefitCostPerYear / paychecksPerYear, 2)
                };
            }

            var dependentCost = dependentCostPerYear * employee.Dependents.Count();

            return new CostPreview
            {
                CostPerPaycheck = Math.Round((benefitCostPerYear + dependentCost) / paychecksPerYear, 2)
            };
        }
    }
}