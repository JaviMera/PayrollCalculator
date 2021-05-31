using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using PayrollCalculator.Web.Models;

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
    }
}
