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
        public void AddSubordinate_Null_ThrowsArgumentNullException()
        {
            var topManager = new Manager("Jason", DateTime.Today);

            Assert.Throws<ArgumentNullException>(() => topManager.AddSubordinate<Employee>(null));
        }

        [Test]
        public void AddSubordinate_This_ThrowsException()
        {
            var topManager = new Manager("Jason", DateTime.Today);

            Assert.Throws<InvalidOperationException>(() => topManager.AddSubordinate(topManager));
        }

        [Test]
        public void AddSubordinate_SimpleCycle_ThrowsException()
        {
            var topManager = new Manager("Jason", DateTime.Today);
            var salesman = new Salesman("James", DateTime.Today);
            topManager.AddSubordinate(salesman);

            Assert.Throws<InvalidOperationException>(() => salesman.AddSubordinate(topManager));
        }

        [Test]
        public void AddSubordinate_ComplexCycle_ThrowsException()
        {
            var topManager = new Manager("Jason", DateTime.Today);

            var salesman = new Salesman("James", DateTime.Today);
            topManager.AddSubordinate(salesman);

            var manager = new Manager("John", DateTime.Today);
            salesman.AddSubordinate(manager);

            Assert.Throws<InvalidOperationException>(() => manager.AddSubordinate(topManager));
        }

        [Test]
        public void AddSubordinate_ComplexCycle_CEO_ThrowsException()
        {
            var ceo = new Manager("josh", DateTime.Today);

            var topManager = new Manager("Jason", DateTime.Today);
            ceo.AddSubordinate(topManager);

            var manager = new Manager("John", DateTime.Today);
            topManager.AddSubordinate(manager);
            ceo.AddSubordinate(manager);

            Assert.Throws<InvalidOperationException>(() => manager.AddSubordinate(ceo));
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
