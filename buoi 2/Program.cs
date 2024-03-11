using System;
using System.Collections.Generic;
using System.Linq;

public class Department
{
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; }
}

public class Employee
{
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; }
    public int Age { get; set; }
    public decimal Salary { get; set; }
    public DateTime Birthday { get; set; }
    public int DepartmentId { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        List<Department> departments = new List<Department>
        {
            new Department { DepartmentId = 1, DepartmentName = "HR" },
            new Department { DepartmentId = 2, DepartmentName = "Finance" },
            new Department { DepartmentId = 3, DepartmentName = "IT" }
        };

        List<Employee> employees = new List<Employee>
        {
            new Employee { EmployeeId = 1, EmployeeName = "John", Age = 30, Salary = 50000, Birthday = new DateTime(1990, 5, 10), DepartmentId = 1 },
            new Employee { EmployeeId = 2, EmployeeName = "Alice", Age = 35, Salary = 60000, Birthday = new DateTime(1985, 3, 15), DepartmentId = 2 },
            new Employee { EmployeeId = 3, EmployeeName = "Bob", Age = 25, Salary = 45000, Birthday = new DateTime(1995, 8, 20), DepartmentId = 1 },
            new Employee { EmployeeId = 4, EmployeeName = "Emily", Age = 28, Salary = 55000, Birthday = new DateTime(1992, 10, 5), DepartmentId = 3 },
            new Employee { EmployeeId = 5, EmployeeName = "David", Age = 40, Salary = 70000, Birthday = new DateTime(1980, 12, 25), DepartmentId = 2 }
        };

        decimal maxSalary = employees.Max(e => e.Salary);
        decimal minSalary = employees.Min(e => e.Salary);
        decimal avgSalary = employees.Average(e => e.Salary);
        Console.WriteLine($"Max Salary: {maxSalary}, Min Salary: {minSalary}, Average Salary: {avgSalary}");

        int maxAge = employees.Max(e => e.Age);
        int minAge = employees.Min(e => e.Age);
        Console.WriteLine($"Max Age: {maxAge}, Min Age: {minAge}");

        var employeeDepartmentJoin = from emp in employees
                                     join dept in departments on emp.DepartmentId equals dept.DepartmentId
                                     select new
                                     {
                                         EmployeeName = emp.EmployeeName,
                                         DepartmentName = dept.DepartmentName
                                     };

        var leftJoin = from dept in departments
                       join emp in employees on dept.DepartmentId equals emp.DepartmentId into empGroup
                       from emp in empGroup.DefaultIfEmpty()
                       select new
                       {
                           DepartmentName = dept.DepartmentName,
                           EmployeeName = emp != null ? emp.EmployeeName : "No employee"
                       };

        var rightJoin = from emp in employees
                        join dept in departments on emp.DepartmentId equals dept.DepartmentId into deptGroup
                        from dept in deptGroup.DefaultIfEmpty()
                        select new
                        {
                            EmployeeName = emp.EmployeeName,
                            DepartmentName = dept != null ? dept.DepartmentName : "No department"
                        };

        Console.WriteLine("Employee-Department Join:");
        foreach (var item in employeeDepartmentJoin)
        {
            Console.WriteLine($"{item.EmployeeName} - {item.DepartmentName}");
        }

        Console.WriteLine("\nLeft Join:");
        foreach (var item in leftJoin)
        {
            Console.WriteLine($"{item.DepartmentName} - {item.EmployeeName}");
        }

        Console.WriteLine("\nRight Join:");
        foreach (var item in rightJoin)
        {
            Console.WriteLine($"{item.EmployeeName} - {item.DepartmentName}");
        }
        Console.ReadLine();
    }
}
