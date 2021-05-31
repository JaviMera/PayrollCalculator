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
        public EmployeeModel Employee { get; set; }

        public IndexModel()
        {     
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            Employee employee = new Employee
            {
                Name = Employee.Name,
                Dependents = new List<Dependent> {
                    new Dependent { Name = Employee.SpouseName},
                    new Dependent { Name = Employee.ChildOne},
                    new Dependent { Name = Employee.ChildTwo},
                }
            };

            return RedirectToPage("/PreviewCost", new { employee = JsonConvert.SerializeObject(employee) });
        }
    }
}
