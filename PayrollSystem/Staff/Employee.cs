using System;
using System.Collections.Generic;

namespace PayrollSystem.Staff
{
    public class Employee
    {
        public const int DaysInYear = 365;
        public const decimal DefaultBaseRate = 1000;
        private string name;
        private decimal baseRate = DefaultBaseRate;
        private readonly IList<Supervisor> supervisors = new List<Supervisor>();
        private static int counter;

        public IEnumerable<Supervisor> Supervisors => supervisors;

        public int Id { get; private set; }

        public string Name
        {
            get => name;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(Name));
                }

                if (value.Length == 0)
                {
                    throw new ArgumentException(nameof(Name) + " can not be empty", nameof(Name));
                }

                name = value;
            }
        }
        public DateTime HireDate { get; private set; }
        public decimal BaseRate
        {
            get => baseRate;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(BaseRate), nameof(BaseRate) + " can not be negative");
                }

                baseRate = value;
            }
        }
        public virtual decimal AnnualBonusRate => 0.03m;
        public virtual decimal MaxBonusRate => 0.3m;

        public Employee(string name, DateTime hireDate)
        {
            Name = name;
            HireDate = hireDate;
            BaseRate = DefaultBaseRate;
            Id = counter++;
        }

        public Employee(string name, DateTime hireDate, decimal baseRate) : this(name, hireDate)
        {
            BaseRate = baseRate;
        }

        public decimal GetSalary()
        {
            return GetSalary(DateTime.Today);
        }

        public virtual decimal GetSalary(DateTime date, IDictionary<int, decimal> salaries)
        {
            if (!salaries.ContainsKey(Id))
            {
                salaries.Add(Id, GetSalary(date));
            }

            return salaries[Id];
        }

        public virtual decimal GetSalary(DateTime date)
        {
            if (date < HireDate)
            {
                throw new ArgumentException($"{nameof(date)} ({date}) must be greater or equal to {nameof(HireDate)} ({HireDate})", nameof(date));
            }

            var years = GetYears(date);
            return BaseRate * (1 + Math.Min(years * AnnualBonusRate, MaxBonusRate));
        }

        private int GetYears(DateTime date)
        {
            return ((int)(date - HireDate).TotalDays) / DaysInYear;
        }

        public T AddSupervisor<T>(T supervisor) where T : Supervisor
        {
            if (supervisor == null)
            {
                throw new ArgumentNullException(nameof(supervisor));
            }

            if (supervisor == this)
            {
                throw new InvalidOperationException("The supervisor can not be the supervisor for himself");
            }

            supervisors.Add(supervisor);

            if (!supervisor.HasSubordinate(this))
            {
                supervisor.AddSubordinate(this);
            }

            return supervisor;
        }

        public T RemoveSupervisor<T>(T supervisor) where T : Supervisor
        {
            supervisors.Remove(supervisor);

            if (supervisor.HasSubordinate(this))
            {
                supervisor.RemoveSubordinate(this);
            }

            return supervisor;
        }

        public bool HasSupervisor(Supervisor supervisor)
        {
            return supervisors.Contains(supervisor);
        }

        public IEnumerable<Employee> GetAllSupervisors()
        {
            return GetAllSupervisors(this);
        }

        private static IEnumerable<Employee> GetAllSupervisors(Employee employee)
        {
            foreach (var supervisor in employee.Supervisors)
            {
                yield return supervisor;

                foreach (var s in GetAllSupervisors(supervisor))
                {
                    yield return s;
                }
            }
        }
    }
}
