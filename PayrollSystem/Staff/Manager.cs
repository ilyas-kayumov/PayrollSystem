using System;
using System.Collections.Generic;
using System.Linq;

namespace PayrollSystem.Staff
{
    public class Manager : Supervisor
    {
        public const decimal EmployeesBonusRate = 0.005m;
        public override decimal AnnualBonusRate => 0.05m;
        public override decimal MaxBonusRate => 0.4m;

        public Manager(string name, DateTime hireDate) : base(name, hireDate)
        {
        }

        public Manager(string name, DateTime hireDate, decimal baseRate) : base(name, hireDate, baseRate)
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
            return base.GetSalary(date) + EmployeesBonusRate * Subordinates.Sum(e => e.GetSalary(date, salaries));
        }
    }
}
