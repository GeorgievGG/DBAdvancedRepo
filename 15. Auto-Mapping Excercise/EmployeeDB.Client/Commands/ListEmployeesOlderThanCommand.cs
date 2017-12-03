using EmployeeDB.Client.Commands.Contracts;
using EmployeeDB.Services.Contracts;
using System.Text;

namespace EmployeeDB.Client.Commands
{
    public class ListEmployeesOlderThanCommand : ICommand
    {
        private readonly IEmployeeService empSrv;

        public ListEmployeesOlderThanCommand(IEmployeeService empSrv)
        {
            this.empSrv = empSrv;
        }

        public string Execute(string[] data)
        {
            var reqAge = int.Parse(data[0]);

            var employees = empSrv.GetAllEmployeesAboveAge(reqAge);

            var sb = new StringBuilder();

            foreach (var employee in employees)
            {
                sb.AppendLine(employee.ToString());
            }

            return sb.ToString().Trim();
        }
    }
}
