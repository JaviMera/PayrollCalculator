using System.Collections.Generic;

namespace PayrollCalculator.Data
{
    public class EmployeeEntity
    {        
        public int Id { get; set; }

        public string Name { get; set; }

        public List<DependentEntity> Dependents { get; set; }
    }
}
