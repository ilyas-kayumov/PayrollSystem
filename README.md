# Payroll System

A class library for calculating payroll (salary).

# How to use

Example:

```
var company = new Company();
var salary = company.Add<Manager>("John").GetSalary(DateTime.Today); 
Console.WriteLine(salary);

company.Add<Employee>("Jack"); 
company.Add<Salesman>("James");

var payroll = company.GetTotalPayroll(DateTime.Today);
Console.WriteLine(payroll);
```
