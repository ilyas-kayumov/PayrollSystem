using System;
using System.Collections.Generic;

namespace PayrollSystem.Staff
{
    public abstract class Supervisor : Employee
    {
        private readonly IList<Employee> employees = new List<Employee>();
        public IEnumerable<Employee> Subordinates => employees;

        public Supervisor(string name, DateTime hireDate) : base(name, hireDate)
        {
        }

        public Supervisor(string name, DateTime hireDate, decimal baseRate) : base(name, hireDate, baseRate)
        {
        }

        public T AddSubordinate<T>(T employee) where T: Employee
        {
            employees.Add(employee);
            return employee;
        }

        public T RemoveSubordinate<T>(T employee) where T: Employee
        {
            employees.Remove(employee);
            return employee;
        }

        public bool HasSubordinate(Employee employee)
        {
            return employees.Contains(employee);
        }
    }
}
