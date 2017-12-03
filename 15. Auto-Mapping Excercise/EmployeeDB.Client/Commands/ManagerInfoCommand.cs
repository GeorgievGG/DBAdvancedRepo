using EmployeeDB.Client.Commands.Contracts;
using EmployeeDB.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeDB.Client.Commands
{
    public class ManagerInfoCommand : ICommand
    {
        private readonly IEmployeeService empSrv;

        public ManagerInfoCommand(IEmployeeService empSrv)
        {
            this.empSrv = empSrv;
        }

        public string Execute(string[] data)
        {
            var mgrId = int.Parse(data[0]);

            var emp = empSrv.ManagerInfoById(mgrId);

            return emp.ToString();
        }
    }
}
