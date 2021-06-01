using System.ComponentModel.DataAnnotations;

namespace PayrollCalculator.Data
{
    public class DependentEntity
    {        
        public int Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public virtual EmployeeEntity Employee { get; set; }
        public virtual int EmployeeId { get; set; }
    }
}
