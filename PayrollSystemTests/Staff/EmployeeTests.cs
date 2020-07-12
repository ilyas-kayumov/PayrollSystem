using NUnit.Framework;
using PayrollSystem.Staff;
using System;

namespace PayrollSystemTests.Staff
{
    [TestFixture]
    public class EmployeeTests
    {
        private Employee employee;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var hireDate = new DateTime(2020, 7, 11);
            employee = new Employee("Jack", hireDate);
        }

        [Test]
        public void GetSalary_LessThenYear_BaseRate()
        {
            var date = new DateTime(2021, 6, 11);

            var salary = employee.GetSalary(date);

            Assert.AreEqual(employee.BaseRate, salary);

        }

        [Test]
        public void GetSalary_MoreThenYear_BaseRateAndBonus()
        {
            var date = new DateTime(2021, 8, 11);
            var baseRateAndBonus = employee.BaseRate * (1 + employee.AnnualBonusRate);

            var salary = employee.GetSalary(date);

            Assert.AreEqual(baseRateAndBonus, salary);
        }

        [Test]
        public void GetSalary_MoreThen2Years_BaseRateAndBonus()
        {
            var date = new DateTime(2022, 9, 11);
            var baseRateAndBonus = employee.BaseRate * (1 + 2 * employee.AnnualBonusRate);

            var salary = employee.GetSalary(date);

            Assert.AreEqual(baseRateAndBonus, salary);
        }

        [Test]
        public void GetSalary_MoreThen10Years_BaseRateAndBonus()
        {
            var date = new DateTime(2030, 8, 11);
            var baseRateAndBonus = employee.BaseRate * (1 + 10 * employee.AnnualBonusRate);

            var salary = employee.GetSalary(date);

            Assert.AreEqual(baseRateAndBonus, salary);
        }

        [Test]
        public void GetSalary_MoreThen20Years_BaseRateAndMaxBonus()
        {
            var date = new DateTime(2040, 8, 11);
            var baseRateAndBonus = employee.BaseRate * (1 + employee.MaxBonusRate);

            var salary = employee.GetSalary(date);

            Assert.AreEqual(baseRateAndBonus, salary);
        }

        [Test]
        public void GetSalary_InvalidDate_ThrowsInvalidException()
        {
            var invalidDate = new DateTime(1, 1, 1);

            Assert.Throws<ArgumentException>(() => employee.GetSalary(invalidDate));
        }

        [Test]
        public void SetName_NullName_ThrowNullException()
        {
            Assert.Throws<ArgumentNullException>(() => employee.Name = null);
        }

        [Test]
        public void SetName_EmptyName_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => employee.Name = "");
        }

        [Test]
        public void SetName_ValidName_NoException()
        {
            employee.Name = "Jack";
        }

        [Test]
        public void SetBaseRate_NegativeValue_ThrowsOutOfRangeException()
        {
            var jon = new Employee("Jon", DateTime.Today);

            Assert.Throws<ArgumentOutOfRangeException>(() => jon.BaseRate = -1000);
        }

        [Test]
        public void SetBaseRate_PositiveValue_NoException()
        {
            new Employee("Jon", DateTime.Today) { BaseRate = 1000 };
        }
    }
}
