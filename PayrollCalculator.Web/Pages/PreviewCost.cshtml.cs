using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using PayrollCalculator.Services;
using PayrollCalculator.Services.Models;
using PayrollCalculator.Web.Models;

namespace PayrollCalculator.Web.Pages
{
    public class PreviewCostModel : PageModel
    {
        private readonly IPayrollCalculatorService _payrollCalculatorService;

        [BindProperty]
        public PreviewModel Preview { get; set; }

        public PreviewCostModel(IPayrollCalculatorService payrollCalculatorService)
        {
            _payrollCalculatorService = payrollCalculatorService;
        }

        public IActionResult OnGet(string employeeJson)
        {
            Preview = new PreviewModel();

            var employee = JsonConvert.DeserializeObject<Employee>(employeeJson);            
            var preview = _payrollCalculatorService.CalculateEmployeeCosts(employee);

            Preview.EmployeeName = employee.Name;
            Preview.EmployeeCost = preview.EmployeeCost;
            Preview.DependentCost = preview.DependentCost;
            Preview.TotalCost = preview.TotalCost;

            return Page();
        }        

        public IActionResult OnPostBackTo()
        {
            return RedirectToPage("/Index", new { employee = string.Empty });
        }
    }
}
