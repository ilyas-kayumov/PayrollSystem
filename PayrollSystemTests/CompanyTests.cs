using NUnit.Framework;
using PayrollSystem;
using PayrollSystem.Staff;
using System.Linq;

namespace PayrollSystemTests
{
    [TestFixture]
    public class CompanyTests
    {
        [Test]
        public void GetTotalPayroll_NoEmployees_0()
        {
            var payroll = new Company().GetTotalPayroll();

            Assert.AreEqual(0, payroll);
        }

        [Test]
        public void GetTotalPayroll_1Employee_Salary()
        {
            var company = new Company();
            var salary = company.Add<Employee>("Jack").GetSalary();

            var payroll = company.GetTotalPayroll();

            Assert.AreEqual(salary, payroll);
        }

        [Test]
        public void GetTotalPayroll_3Employees_TotalSalary()
        {
            var company = new Company();
            var totalSalary = company.Add<Manager>("John").GetSalary() + 
                              company.Add<Employee>("Jack").GetSalary() + 
                              company.Add<Salesman>("James").GetSalary();

            var payroll = company.GetTotalPayroll();

            Assert.AreEqual(totalSalary, payroll);
        }

        [Test]
        public void GetTotalPayroll_Salesman_TotalSalary()
        {
            // Arrange
            var company = new Company();
            var john = company.Add<Manager>("John");

            var jack = company.Add<Salesman>("John");
            john.AddSubordinate(jack);

            var james = company.Add<Salesman>("James");
            jack.AddSubordinate(james);

            var jo = company.Add<Salesman>("Jo");
            jack.AddSubordinate(jo);

            var jastin = company.Add<Manager>("Jastin");
            jo.AddSubordinate(jastin);

            var totalSalary = company.Employees.Sum(e => e.GetSalary());

            // Act
            var payroll = company.GetTotalPayroll();

            // Assert
            Assert.AreEqual(totalSalary, payroll);
        }

        [Test]
        public void Add_3Employees_3()
        {
            var company = new Company();

            company.Add<Manager>("John");
            company.Add<Employee>("Jack");
            company.Add<Salesman>("James");

            Assert.AreEqual(3, company.Employees.Count());
        }

        [Test]
        public void Add_3Employees2Removed_1()
        {
            var company = new Company();

            var manager = company.Add<Manager>("John");
            company.Add<Employee>("Jack");
            var salesman = company.Add<Salesman>("James");
            company.Remove(manager);
            company.Remove(salesman);

            Assert.AreEqual(1, company.Employees.Count());
        }

        [Test]
        public void Remove_1Subordinate_SubordinateRemoved()
        {
            var company = new Company();
            var subordinate = company.Add<Employee>("Jack");
            var manager = company.Add<Manager>("John");
            manager.AddSubordinate(subordinate);

            company.Remove(subordinate);

            Assert.IsFalse(manager.HasSubordinate(subordinate));
        }
    }
}
