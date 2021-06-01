using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PayrollCalculator.Data;
using PayrollCalculator.Services;
using PayrollCalculator.Services.Models;
using PayrollCalculator.Web.Models;
using System.Linq;

namespace PayrollCalculator.Web.Pages
{
    public class PreviewCostModel : PageModel
    {
        private readonly PayrollCalculatorDbContext _context;
        private readonly IPayrollCalculatorService _payrollCalculatorService;

        [BindProperty]
        public PreviewModel Preview { get; set; }

        public PreviewCostModel(IPayrollCalculatorService payrollCalculatorService, PayrollCalculatorDbContext context)
        {
            _payrollCalculatorService = payrollCalculatorService;
            _context = context;
        }

        public IActionResult OnGet(int employeeId)
        {
            Preview = new PreviewModel();

            var employeeEntity = _context.Employees
                .Include(employee => employee.Dependents)
                .FirstOrDefault(employee => employee.Id == employeeId);                
            
            var preview = _payrollCalculatorService.CalculateEmployeeCosts(new Employee {
                Name = employeeEntity.Name,
                Dependents = employeeEntity.Dependents.Select(dependent => new Dependent {
                    Name = dependent.Name,
                    Type = dependent.Type
                }).ToList()
            });

            employeeEntity.PreviewCost = new PreviewCostEntity { 
                EmployeeCost = preview.EmployeeCost,
                DependentCost = preview.DependentCost,
                TotalCost = preview.TotalCost
            }; 

            _context.Update(employeeEntity);
            _context.SaveChanges();

            Preview.EmployeeName = employeeEntity.Name;
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
