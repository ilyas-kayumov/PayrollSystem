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
