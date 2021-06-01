namespace PayrollCalculator.Data
{
    public class PreviewCostEntity
    {
        public int Id { get; set; }
        public double EmployeeCost { get; set; }
        public double DependentCost { get; set; }
        public double TotalCost { get; set; }
        public virtual EmployeeEntity Employee { get; set; }
        public virtual int EmployeeId { get; set; }
    }
}
