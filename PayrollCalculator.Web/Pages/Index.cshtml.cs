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

        public IActionResult OnGet(string employeeJson)
        {
            if (!string.IsNullOrWhiteSpace(employeeJson))
                Employee = JsonConvert.DeserializeObject<EmployeeModel>(employeeJson);

            return Page();
        }

        public IActionResult OnPostAdd()
        {
            Employee.Dependents.Add(new DependentModel());
            return RedirectToPage("/Index", new { employeeJson = JsonConvert.SerializeObject(Employee)});
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

            return RedirectToPage("/PreviewCost", new { employeeJson = JsonConvert.SerializeObject(employee) });
        }
    }
}
