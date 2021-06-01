using System.Collections.Generic;
using System.Linq;

namespace PayrollCalculator.Data
{
    public sealed class PayrollCalculatorDbSeeder
    {
        private readonly PayrollCalculatorDbContext _context;

        public PayrollCalculatorDbSeeder(PayrollCalculatorDbContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (!_context.Database.CanConnect())
            {
                return;
            }

            if (!_context.Employees.Any())
            {
                _context.Employees.AddRange(new List<EmployeeEntity>
            {
                new EmployeeEntity
                {
                    Name = "Jack",
                    Dependents = new List<DependentEntity>
                    {
                        new DependentEntity
                        {
                            Name = "Wendy",
                            Type = "Spouse"
                        },
                        new DependentEntity
                        {
                            Name = "Danny",
                            Type = "Child"
                        }
                    }
                },
                new EmployeeEntity
                {
                    Name = "Albert",
                    Dependents = new List<DependentEntity>
                    {
                        new DependentEntity
                        {
                            Name = "Maric",
                            Type = "Spouse"
                        }
                    }
                },
                new EmployeeEntity
                {
                    Name = "Alicia",
                    Dependents = new List<DependentEntity>
                    {
                        new DependentEntity
                        {
                            Name = "Aaron",
                            Type = "Child"
                        },
                        new DependentEntity
                        {
                            Name = "Chris",
                            Type = "Child"
                        }
                    }
                }
            });

                _context.SaveChanges();
            }

            if (!_context.PreviewCosts.Any())
            {
                foreach(var employee in _context.Employees)
                {
                    employee.PreviewCost = new PreviewCostEntity
                    {
                        EmployeeCost = 10,
                        DependentCost = 60,
                        TotalCost = 70
                    };
                }

                _context.SaveChanges();
            }
        }
    }
}
