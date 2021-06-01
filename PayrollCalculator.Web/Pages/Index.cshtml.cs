using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PayrollCalculator.Data;
using PayrollCalculator.Services.Models;
using PayrollCalculator.Web.Models;
using System.Collections.Generic;
using System.Linq;

namespace PayrollCalculator.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly PayrollCalculatorDbContext _context;

        public IndexModel(PayrollCalculatorDbContext context)
        {
            _context = context;
        }

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

            _context.Employees.Add(new EmployeeEntity
            {
                Name = employee.Name,
                Dependents = employee.Dependents.Select(dependent => new DependentEntity
                {
                    Name = dependent.Name,
                    Type = dependent.Type
                }).ToList()
            });

            _context.SaveChanges();

            return RedirectToPage("/PreviewCost", new { employeeId = _context.Employees.OrderByDescending(employee => employee.Id).FirstOrDefault()?.Id });
        }
    }
}
