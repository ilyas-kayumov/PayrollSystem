using System;
using System.Collections.Generic;
using System.Linq;

namespace PayrollSystem.Staff
{
    public class Salesman : Supervisor
    {
        public const decimal EmployeesBonusRate = 0.003m;
        public override decimal AnnualBonusRate => 0.01m;
        public override decimal MaxBonusRate => 0.35m;

        public Salesman(string name, DateTime hireDate) : base(name, hireDate)
        {
        }

        public Salesman(string name, DateTime hireDate, decimal baseRate) : base(name, hireDate, baseRate)
        {
        }

        public override decimal GetSalary(DateTime date)
        {
            return GetSalary(date, new Dictionary<int, decimal>());
        }

        public override decimal GetSalary(DateTime date, IDictionary<int, decimal> salaries)
        {
            if (!salaries.ContainsKey(Id))
            {
                salaries.Add(Id, GetSalaryPrivate(date, salaries));
            }

            return salaries[Id];
        }

        private decimal GetSalaryPrivate(DateTime date, IDictionary<int, decimal> salaries)
        {
            return base.GetSalary(date) + EmployeesBonusRate * GetAllEmployees().Sum(e => e.GetSalary(date, salaries));
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return GetAllEmployees(this);
        }

        private static IEnumerable<Employee> GetAllEmployees(Supervisor supervisor)
        {
            foreach (var employee in supervisor.Subordinates)
            {
                yield return employee;

                if (employee is Supervisor s)
                {
                    foreach (var e in GetAllEmployees(s))
                    {
                        yield return e;
                    }
                }
            }
        }
    }
}
