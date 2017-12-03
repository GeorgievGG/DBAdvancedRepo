using EmployeeDB.Client.Commands.Contracts;
using EmployeeDB.Services.Contracts;

namespace EmployeeDB.Client.Commands
{
    public class EmployeeInfo : ICommand
    {
        private readonly IEmployeeService empSrv;

        public EmployeeInfo(IEmployeeService empSrv)
        {
            this.empSrv = empSrv;
        }

        public string Execute(string[] data)
        {
            var empId = int.Parse(data[0]);

            var emp = empSrv.ByID(empId);

            return emp.ToString();
        }
    }
}
