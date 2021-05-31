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

        public IActionResult OnGet(string employee)
        {
            Preview = new PreviewModel();

            var e = JsonConvert.DeserializeObject<Employee>(employee);            
            var p = _payrollCalculatorService.CalculateEmployeeCosts(e);

            Preview.EmployeeName = e.Name;
            Preview.EmployeeCost = p.EmployeeCost;
            Preview.DependentCost = p.DependentCost;
            Preview.TotalCost = p.TotalCost;

            return Page();
        }        
    }
}
