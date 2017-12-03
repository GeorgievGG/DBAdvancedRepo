using EmployeeDB.Client.Commands.Contracts;
using EmployeeDB.Services.Contracts;

namespace EmployeeDB.Client.Commands
{
    public class AddEmployeeCommand : ICommand
    {
        private readonly IEmployeeService empSrv;

        public AddEmployeeCommand(IEmployeeService empSrv)
        {
            this.empSrv = empSrv;
        }

        public string Execute(string[] data)
        {
            var firstName = data[0];
            var lastName = data[1];
            var salary = decimal.Parse(data[2]);

            var emp = empSrv.AddEmployee(firstName, lastName, salary);

            return emp.ToString();
        }
    }
}
