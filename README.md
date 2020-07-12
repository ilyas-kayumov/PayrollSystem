# Payroll System

A class library for calculating payroll (salary).

# How to use

Example:

```
var company = new Company();
var salary = company.Add<Manager>("John", DateTime.Today).GetSalary(DateTime.Today); 
Console.WriteLine(salary);

company.Add<Employee>("Jack", DateTime.Today); 
company.Add<Salesman>("James", DateTime.Today);

var payroll = company.GetTotalPayroll(DateTime.Today);
Console.WriteLine(payroll);
```

DateTime.Today can be skipped:

```
var company = new Company();
var salary = company.Add<Manager>("John").GetSalary(); 
Console.WriteLine(salary);
```

# Review

Employee is a base class for all kinds of employees, it has common fields and provides a basic algorithm of calculating salary.
Supervisor is a subclass of Employee, it has a list of subordinates. 
Manager and Sales are child classes of Supervisor, they provide own implementation of calculating salary.
Company class holds all employees and provides methods for creating and adding new employees and a method for getting total payroll (sum of salaries).

Pros:
- Relationships between the classes are intuitive and logic. For example, it's clear, that a manager is a supervisor and supervisor is an employee.
- Minimum code duplication, because common logic is provided by the parent classes (Employee, Supervisor) and reused by the child classes (Manager, Salesman).
- Easy to add new types, it's easy to add a subclasses of the existing classes to create new types of employees (for instance: Courier, Engineer, TopManager). 
- Easy to add new methods, for example, add methods for calculating additional fines and bonuses.
- Convenient and safe API. For instance, here's how one can create create an employee, add him to a company and get a salary for today in one line:
```
company.Add<Employee>("Jack").GetSalary();
```
Cons:
- Amount of the classes. Actually, it's possible to solve the task without creating the classes for Supervisor, Manager, Salesman. Alternatively, Employee could take a delegate as a parameter, which calculates a salary.

Additional Notes:

- A conventional year ACT/365 (one year is equal to 365 days) is used in calculating salary. (More information about ACT/365: https://wiki.treasurers.org/wiki/ACT/365_fixed)
This method may not be appropriate in real-world scenario, depending on business rules.

- Rates for calculating salary are hard-coded in the classes. It might be a good idea to get the rates from a configuration file.

- List class is used for storing employees, that's why delete and search operations may be slow for large collections,
one way to avoid this: replace List to HashSet or Dictionary.
