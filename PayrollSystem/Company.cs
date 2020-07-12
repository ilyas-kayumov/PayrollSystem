using PayrollSystem.Staff;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PayrollSystem
{
    public class Company
    {
        private readonly IList<Employee> employees = new List<Employee>();
        public IEnumerable<Employee> Employees => employees;

        public decimal GetTotalPayroll()
        {
            return GetTotalPayroll(DateTime.Today);
        }

        public decimal GetTotalPayroll(DateTime date)
        {
            return Employees.Sum(e => e.GetSalary(date));
        }

        public T Add<T>(string name) where T : Employee
        {
            return Add<T>(name, DateTime.Today);
        }

        public T Add<T>(string name, DateTime hireDate) where T : Employee
        {
            var employee = CreateEmployee<T>(name, hireDate);

            employees.Add(employee);

            return (T) employee;
        }

        public T Remove<T>(T employee) where T : Employee
        {
            employees.Remove(employee);

            foreach (var supervisor in employees.OfType<Supervisor>().Where(s => s.HasSubordinate(employee)))
            {
                supervisor.RemoveSubordinate(employee);
            }

            return employee;
        }

        public bool Has(Employee employee)
        {
            return employees.Contains(employee);
        }

        private static Employee CreateEmployee<T>(string name, DateTime hireDate) where T : Employee
        {
            Employee employee;
            var type = typeof(T);
            if (type == typeof(Employee))
            {
                employee = new Employee(name, hireDate);
            }
            else if (type == typeof(Manager))
            {
                employee = new Manager(name, hireDate);
            }
            else if (type == typeof(Salesman))
            {
                employee = new Salesman(name, hireDate);
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(type));
            }

            return employee;
        }
    }
}
