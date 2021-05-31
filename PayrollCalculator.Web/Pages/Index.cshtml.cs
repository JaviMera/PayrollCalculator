using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PayrollCalculator.Services.Models;
using PayrollCalculator.Web.Models;
using System.Collections.Generic;

namespace PayrollCalculator.Web.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public EmployeeModel Employee { get; set; } = new EmployeeModel();

        public IActionResult OnGet(int dependents)
        {
            Employee = new EmployeeModel();
            Employee.Dependents = new List<DependentModel>();

            for (int i = 0; i < dependents; i++)
            {
                Employee.Dependents.Add(new DependentModel());
            }

            return Page();
        }

        public IActionResult OnPostAdd()
        {
            return RedirectToPage("/Index", new { dependents = Employee.Dependents.Count + 1 });
        }

        public IActionResult OnPost()
        {
            Employee employee = new Employee
            {
                Name = Employee.Name
            };

            foreach(var dependent in Employee.Dependents)
            {
                employee.Dependents.Add(new Dependent { Name = dependent.Name });
            }

            return RedirectToPage("/PreviewCost", new { employee = JsonConvert.SerializeObject(employee) });
        }
    }
}
