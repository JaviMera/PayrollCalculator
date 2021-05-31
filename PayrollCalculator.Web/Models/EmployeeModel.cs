using System.Collections.Generic;

namespace PayrollCalculator.Web.Models
{
    public sealed class EmployeeModel
    {
        public string Name { get; set; }
        public List<DependentModel> Dependents { get; set; } = new List<DependentModel>();
        public string SpouseName { get; set; }
        public string ChildOne { get; set; }
        public string ChildTwo { get; set; }
    }
}
