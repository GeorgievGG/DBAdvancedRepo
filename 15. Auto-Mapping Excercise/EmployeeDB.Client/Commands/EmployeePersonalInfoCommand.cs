using EmployeeDB.Client.Commands.Contracts;
using EmployeeDB.Services.Contracts;
using System;

namespace EmployeeDB.Client.Commands
{
    public class EmployeePersonalInfoCommand : ICommand
    {
        private readonly IEmployeeService empSrv;

        public EmployeePersonalInfoCommand(IEmployeeService empSrv)
        {
            this.empSrv = empSrv;
        }

        public string Execute(string[] data)
        {
            var empId = int.Parse(data[0]);

            var emp = empSrv.PersonalInfoByID(empId);

            return emp.ToString();
        }
    }
}
