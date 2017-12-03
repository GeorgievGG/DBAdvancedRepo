using EmployeeDB.Client.DTOs;
using EmployeeDB.Services.DTOs;
using System;
using System.Collections.Generic;

namespace EmployeeDB.Services.Contracts
{
    public interface IEmployeeService
    {
        EmployeeDto ByID(int id);

        PesonalInfoDto PersonalInfoByID(int id);
        ManagerDto ManagerInfoById(int id);
        EmployeeManagerDto EmployeeManagerById(int id);
        List<EmployeeManagerDto> GetAllEmployeesAboveAge(int age);

        EmployeeDto AddEmployee(string firstName, string lastName, decimal salary);

        EmployeeDto AddEmployee(string firstName, string lastName, decimal salary, DateTime birthdate);

        EmployeeDto AddEmployee(string firstName, string lastName, decimal salary, string address);

        EmployeeDto AddEmployee(string firstName, string lastName, decimal salary, DateTime birthdate, string address);

        void UpdateEmployee(PesonalInfoDto emp);
        void SetManager(EmployeeManagerDto emp);
    }
}
