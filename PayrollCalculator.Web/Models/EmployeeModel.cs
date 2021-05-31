using System.Collections.Generic;

namespace PayrollCalculator.Web.Models
{
    public sealed class EmployeeModel
    {
        public string Name { get; set; }
        public List<DependentModel> Dependents { get; set; } = new List<DependentModel>();        
    }
}
