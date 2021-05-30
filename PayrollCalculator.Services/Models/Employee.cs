using System.Collections.Generic;

namespace PayrollCalculator.Services.Models
{
    public sealed class Employee
    {
        public string Name { get; set; } = string.Empty;
        public IEnumerable<Dependent> Dependents { get; set; } = new List<Dependent>();
    }
}
