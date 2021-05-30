using PayrollCalculator.Services.Models;

namespace PayrollCalculator.Services
{
    public interface IPayrollCalculatorService
    {
        CostPreview CalculateEmployeeCosts(Employee employee);
    }
}
