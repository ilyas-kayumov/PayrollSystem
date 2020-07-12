using NUnit.Framework;
using PayrollSystem.Staff;
using System;

namespace PayrollSystemTests.Staff
{
    [TestFixture]
    public class SupervisorTests
    {
        [Test]
        public void AddSubordinate_1Employee_HasSubordinate()
        {
            var manager = new Manager("John", DateTime.Today);
            var employee = new Employee("Jack", DateTime.Today);

            manager.AddSubordinate(employee);

            Assert.IsTrue(manager.HasSubordinate(employee));
        }

        [Test]
        public void RemoveSubordinate_1Employee_HasNoSubordinate()
        {
            var manager = new Manager("John", DateTime.Today);
            var employee = new Employee("Jack", DateTime.Today);
            manager.AddSubordinate(employee);

            manager.RemoveSubordinate(employee);

            Assert.IsFalse(manager.HasSubordinate(employee));
        }

        [Test]
        public void HasEmployee_NoEmployees_HasNoSubordinate()
        {
            var manager = new Manager("John", DateTime.Today);
            var employee = new Employee("Jack", DateTime.Today);

            Assert.IsFalse(manager.HasSubordinate(employee));
        }
    }
}
