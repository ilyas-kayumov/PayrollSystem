using System;
using System.Collections.Generic;
using System.Linq;

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
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            if (employee == this)
            {
                throw new InvalidOperationException("The subordinate can not be the subordinate for himself");
            }

            if (employee is Supervisor supervisor)
            {
                if (supervisor.HasSubordinate(this) || GetAllSupervisors().Any(s => s == employee))
                {
                    throw new InvalidOperationException("The subordinate can not be the supervisor");
                }
            }

            employees.Add(employee);

            if (!employee.HasSupervisor(this))
            {
                employee.AddSupervisor(this);
            }

            return employee;
        }

        public T RemoveSubordinate<T>(T employee) where T: Employee
        {
            employees.Remove(employee);

            if (employee.HasSupervisor(this))
            {
                employee.RemoveSupervisor(this);
            }

            return employee;
        }

        public bool HasSubordinate(Employee employee)
        {
            return employees.Contains(employee);
        }
    }
}
