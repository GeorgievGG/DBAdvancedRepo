using AutoMapper;
using EmployeeDB.Client.DTOs;
using EmployeeDB.Data;
using EmployeeDB.Models;
using EmployeeDB.Services.Contracts;
using EmployeeDB.Services.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;

namespace EmployeeDB.Services.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly EmployeeContext context;

        public EmployeeService(EmployeeContext context)
        {
            this.context = context;
        }

        public EmployeeDto AddEmployee(string firstName, string lastName, decimal salary)
        {
            var emp = new Employee
            {
                FirstName = firstName,
                LastName = lastName,
                Salary = salary
            };

            context.Add(emp);
            context.SaveChanges();

            return Mapper.Map<EmployeeDto>(emp);
        }

        public EmployeeDto AddEmployee(string firstName, string lastName, decimal salary, DateTime birthday)
        {
            var emp = new Employee
            {
                FirstName = firstName,
                LastName = lastName,
                Salary = salary,
                Birthday = birthday
            };

            context.Add(emp);
            context.SaveChanges();

            return Mapper.Map<EmployeeDto>(emp);
        }

        public EmployeeDto AddEmployee(string firstName, string lastName, decimal salary, string address)
        {
            var emp = new Employee
            {
                FirstName = firstName,
                LastName = lastName,
                Salary = salary,
                Address = address
            };

            context.Add(emp);
            context.SaveChanges();

            return Mapper.Map<EmployeeDto>(emp);
        }

        public EmployeeDto AddEmployee(string firstName, string lastName, decimal salary, DateTime birthday, string address)
        {
            var emp = new Employee
            {
                FirstName = firstName,
                LastName = lastName,
                Salary = salary,
                Birthday = birthday,
                Address = address
            };

            context.Add(emp);
            context.SaveChanges();

            return Mapper.Map<EmployeeDto>(emp);
        }

        public EmployeeDto ByID(int id)
        {
            return Mapper.Map<EmployeeDto>(context.Employees.SingleOrDefault(e => e.Id == id));
        }

        public PesonalInfoDto PersonalInfoByID(int id)
        {
            return Mapper.Map<PesonalInfoDto>(context.Employees.SingleOrDefault(e => e.Id == id));
        }

        public ManagerDto ManagerInfoById(int id)
        {
            var manager = context.Employees.SingleOrDefault(e => e.Id == id);
            return Mapper.Map<ManagerDto>(manager);
        }
        public EmployeeManagerDto EmployeeManagerById(int id)
        {
            var employeeManager = context.Employees.SingleOrDefault(e => e.Id == id);
            return Mapper.Map<EmployeeManagerDto>(employeeManager);
        }

        public void UpdateEmployee(PesonalInfoDto emp)
        {
            var empToReplace = context.Employees.SingleOrDefault(e => e.Id == emp.Id);
            if (empToReplace.Birthday != emp.Birthday)
            {
                empToReplace.Birthday = emp.Birthday;
            }
            else if (empToReplace.Address != emp.Address)
            {
                empToReplace.Address = emp.Address;
            }
            context.SaveChanges();
        }

        public void SetManager(EmployeeManagerDto emp)
        {
            var empToReplace = context.Employees.SingleOrDefault(e => e.Id == emp.Id);
            empToReplace.ManagerId = emp.ManagerId;
            context.SaveChanges();
        }

        public List<EmployeeManagerDto> GetAllEmployeesAboveAge(int age)
        {
            var employees = context.Employees.Where(e => e.Age >= age);

            return employees
                .OrderByDescending(e => e.Salary)
                .Select(e => Mapper.Map<EmployeeManagerDto>(e))
                .ToList();
        }
    }
}
