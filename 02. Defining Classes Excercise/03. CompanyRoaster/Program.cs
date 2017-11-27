using System;
using System.Collections.Generic;
using System.Linq;

namespace _03.CompanyRoaster
{
    public class Program
    {
        public static void Main()
        {
            var n = int.Parse(Console.ReadLine());
            var employees = new Dictionary<int, Employee>();

            for (int i = 0; i < n; i++)
            {
                var inputParams = Console.ReadLine().Split();

                if (inputParams.Length == 4)
                {
                    employees.Add(i, new Employee(inputParams[0], decimal.Parse(inputParams[1]), inputParams[2], inputParams[3]));
                }
                else if (inputParams.Length == 5)
                {
                    var age = 0;
                    if (int.TryParse(inputParams[4], out age))
                    {
                        employees.Add(i, new Employee(inputParams[0], decimal.Parse(inputParams[1]), inputParams[2], inputParams[3], int.Parse(inputParams[4])));
                    }
                    else
                    {
                        employees.Add(i, new Employee(inputParams[0], decimal.Parse(inputParams[1]), inputParams[2], inputParams[3], inputParams[4]));
                    }
                }
                else
                {
                    employees.Add(i, new Employee(inputParams[0], decimal.Parse(inputParams[1]), inputParams[2], inputParams[3], inputParams[4], int.Parse(inputParams[5])));
                }
            }
            
            var highestDeptBySalary = employees.Values.GroupBy(x => x.Department, x => x.Salary, (key, g) => new { Department = key, AvgSalary = g.Sum() }).OrderByDescending(x => x.AvgSalary).FirstOrDefault();

            Console.WriteLine($"Highest Average Salary: {highestDeptBySalary.Department}");

            foreach (var emp in employees.Values.Where(x => x.Department == highestDeptBySalary.Department).OrderByDescending(x => x.Salary))
            {
                Console.WriteLine(emp);
            }
        }
    }
}
