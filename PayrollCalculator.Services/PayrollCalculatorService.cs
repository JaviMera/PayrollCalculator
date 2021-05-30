using PayrollCalculator.Services.Models;

namespace PayrollCalculator.Services
{
    public sealed class PayrollCalculatorService : IPayrollCalculatorService
    {
        public CostPreview CalculateEmployeeCosts(Employee employee)
        {
            return new CostPreview();
        }
    }
}
