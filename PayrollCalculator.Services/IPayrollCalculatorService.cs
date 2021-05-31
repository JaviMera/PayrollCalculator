using PayrollCalculator.Services.Models;

namespace PayrollCalculator.Services
{
    public interface IPayrollCalculatorService
    {
        Preview CalculateEmployeeCosts(Employee employee);
    }
}
