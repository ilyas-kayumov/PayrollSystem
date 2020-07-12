using System;

namespace PayrollSystem.Staff
{
    public class Employee
    {
        public const int DaysInYear = 365;
        public const decimal DefaultBaseRate = 1000;
        private string name;
        private decimal baseRate = DefaultBaseRate;

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
        }

        public Employee(string name, DateTime hireDate, decimal baseRate) : this(name, hireDate)
        {
            BaseRate = baseRate;
        }

        public decimal GetSalary()
        {
            return GetSalary(DateTime.Today);
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
    }
}
