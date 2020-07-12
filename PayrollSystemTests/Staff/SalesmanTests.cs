using NUnit.Framework;
using PayrollSystem.Staff;
using System;
using System.Linq;

namespace PayrollSystemTests.Staff
{
    [TestFixture]
    public class SalesmanTests
    {
        private Salesman salesman;
        private Manager manager;
        private Employee employee;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var hireDate = new DateTime(2020, 7, 11);
            salesman = new Salesman("James", hireDate);

            manager = new Manager("John", hireDate);
            salesman.AddSubordinate(manager);

            employee = new Employee("Jack", hireDate);
            manager.AddSubordinate(employee);

            employee = new Employee("Jo", hireDate);
            manager.AddSubordinate(employee);
        }

        [Test]
        public void GetSalary_LessThenYear_BaseRateAndBonus()
        {
            var date = new DateTime(2021, 6, 11);
            var employeesBonus = (2 * employee.GetSalary(date) + manager.GetSalary(date)) * Salesman.EmployeesBonusRate;
            var baseRateAndBonus = salesman.BaseRate + employeesBonus;

            var salary = salesman.GetSalary(date);

            Assert.AreEqual(baseRateAndBonus, salary);
        }

        [Test]
        public void GetSalary_MoreThenYear_BaseRateAndBonus()
        {
            var date = new DateTime(2021, 8, 11);
            var employeesBonus = (2 * employee.GetSalary(date) + manager.GetSalary(date)) * Salesman.EmployeesBonusRate;
            var baseRateAndBonus = salesman.BaseRate * (1 + salesman.AnnualBonusRate) + employeesBonus;

            var salary = salesman.GetSalary(date);

            Assert.AreEqual(baseRateAndBonus, salary);
        }

        [Test]
        public void GetSalary_MoreThen2Years_BaseRateAndBonus()
        {
            var date = new DateTime(2022, 9, 11);
            var employeesBonus = (2 * employee.GetSalary(date) + manager.GetSalary(date)) * Salesman.EmployeesBonusRate;
            var baseRateAndBonus = salesman.BaseRate * (1 + 2 * salesman.AnnualBonusRate) + employeesBonus;

            var salary = salesman.GetSalary(date);

            Assert.AreEqual(baseRateAndBonus, salary);
        }

        [Test]
        public void GetSalary_MoreThen10Years_BaseRateAndBonus()
        {
            var date = new DateTime(2030, 8, 11);
            var employeesBonus = (2 * employee.GetSalary(date) + manager.GetSalary(date)) * Salesman.EmployeesBonusRate;
            var baseRateAndBonus = salesman.BaseRate * (1 + 10 * salesman.AnnualBonusRate) + employeesBonus;

            var salary = salesman.GetSalary(date);

            Assert.AreEqual(baseRateAndBonus, salary);
        }

        [Test]
        public void GetSalary_MoreThen20Years_BaseRateAndBonus()
        {
            var date = new DateTime(2040, 8, 11);
            var employeesBonus = (2 * employee.GetSalary(date) + manager.GetSalary(date)) * Salesman.EmployeesBonusRate;
            var baseRateAndBonus = salesman.BaseRate * (1 + 20 * salesman.AnnualBonusRate) + employeesBonus;

            var salary = salesman.GetSalary(date);

            Assert.AreEqual(baseRateAndBonus, salary);
        }

        [Test]
        public void GetSalary_MoreThen40Years_BaseRateAndBonus()
        {
            var date = new DateTime(2060, 8, 11);
            var employeesBonus = (2 * employee.GetSalary(date) + manager.GetSalary(date)) * Salesman.EmployeesBonusRate;
            var baseRateAndBonus = salesman.BaseRate * (1 + salesman.MaxBonusRate) + employeesBonus;

            var salary = salesman.GetSalary(date);

            Assert.AreEqual(baseRateAndBonus, salary);
        }

        [Test]
        public void GetAllEmployees_3NestedEmployees_3Employees()
        {
            var count = salesman.GetAllEmployees().Count();

            Assert.AreEqual(3, count);
        }

        [Test]
        public void GetAllEmployees_5NestedEmployees_5Employees()
        {
            // Arrange
            var hireDate = DateTime.Today;
            var james = new Salesman("James", hireDate);

            var john = new Manager("John", hireDate);
            james.AddSubordinate(john);

            var jack = new Salesman("Jack", hireDate);
            john.AddSubordinate(jack);

            var jo = new Manager("Jo", hireDate);
            jack.AddSubordinate(jo);

            var jon = new Manager("Jon", hireDate);
            jo.AddSubordinate(jon);

            var jastin = new Employee("Jastin", hireDate);
            jo.AddSubordinate(jastin);

            // Act
            var count = james.GetAllEmployees().Count();

            // Assert
            Assert.AreEqual(5, count);
        }
    }
}
