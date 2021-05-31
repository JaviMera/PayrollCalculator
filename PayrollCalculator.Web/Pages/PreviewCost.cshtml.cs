using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using PayrollCalculator.Services.Models;

namespace PayrollCalculator.Web.Pages
{
    public class PreviewCostModel : PageModel
    {
        [BindProperty]
        public string EmployeeName { get; set; }

        public IActionResult OnGet(string employee)
        {
            var e = JsonConvert.DeserializeObject<Employee>(employee);
            EmployeeName = e.Name;

            return Page();
        }        
    }
}
