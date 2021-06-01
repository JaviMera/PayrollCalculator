using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PayrollCalculator.Data;

namespace PayrollCalculator.Web.Pages
{
    public class EmployeesModel : PageModel
    {
        private readonly PayrollCalculatorDbContext _context;

        [BindProperty]
        public List<EmployeeEntity> Employees { get; set; }

        public EmployeesModel(PayrollCalculatorDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            Employees = _context.Employees
                .Include(employee => employee.Dependents)
                .Include(employee => employee.PreviewCost)
                .ToList();
        }
    }
}
