using NUnit.Framework;
using PayrollSystem.Staff;
using System;

namespace PayrollSystemTests.Staff
{
    [TestFixture]
    public class ManagerTests
    {
        private Manager manager;
        private Employee employee;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var hireDate = new DateTime(2020, 7, 11);
            manager = new Manager("John", hireDate);

            employee = new Employee("Jack", hireDate);
            manager.AddSubordinate(employee);

            employee = new Employee("Jo", hireDate);
            manager.AddSubordinate(employee);
        }

        [Test]
        public void GetSalary_LessThenYear_BaseRateAndBonus()
        {
            var date = new DateTime(2021, 6, 11);
            var baseRateAndBonus = manager.BaseRate + 2 * Employee.DefaultBaseRate * Manager.EmployeesBonusRate;

            var salary = manager.GetSalary(date);

            Assert.AreEqual(baseRateAndBonus, salary);
        }

        [Test]
        public void GetSalary_MoreThenYear_BaseRateAndBonus()
        {
            var date = new DateTime(2021, 8, 11);
            var employeesBonus = 2 * employee.GetSalary(date) * Manager.EmployeesBonusRate;
            var baseRateAndBonus = manager.BaseRate * (1 + manager.AnnualBonusRate) + employeesBonus;

            var salary = manager.GetSalary(date);

            Assert.AreEqual(baseRateAndBonus, salary);
        }

        [Test]
        public void GetSalary_MoreThen2Years_BaseRateAndBonus()
        {
            var date = new DateTime(2022, 9, 11);
            var employeesBonus = 2 * employee.GetSalary(date) * Manager.EmployeesBonusRate;
            var baseRateAndBonus = manager.BaseRate * (1 + 2 * manager.AnnualBonusRate) + employeesBonus;

            var salary = manager.GetSalary(date);

            Assert.AreEqual(baseRateAndBonus, salary);
        }

        [Test]
        public void GetSalary_MoreThen10Years_BaseRateAndBonus()
        {
            var date = new DateTime(2030, 8, 11);
            var employeesBonus = 2 * employee.GetSalary(date) * Manager.EmployeesBonusRate;
            var baseRateAndBonus = manager.BaseRate * (1 + manager.MaxBonusRate) + employeesBonus;

            var salary = manager.GetSalary(date);

            Assert.AreEqual(baseRateAndBonus, salary);
        }

        [Test]
        public void GetSalary_MoreThen20Years_BaseRateAndMaxBonus()
        {
            var date = new DateTime(2040, 8, 11);
            var employeesBonus = 2 * employee.GetSalary(date) * Manager.EmployeesBonusRate;
            var baseRateAndBonus = manager.BaseRate * (1 + manager.MaxBonusRate) + employeesBonus;

            var salary = manager.GetSalary(date);

            Assert.AreEqual(baseRateAndBonus, salary);
        }
    }
}
