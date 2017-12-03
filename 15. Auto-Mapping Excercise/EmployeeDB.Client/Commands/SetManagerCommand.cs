using EmployeeDB.Client.Commands.Contracts;
using EmployeeDB.Services.Contracts;

namespace EmployeeDB.Client.Commands
{
    public class SetManagerCommand : ICommand
    {
        private readonly IEmployeeService empSrv;

        public SetManagerCommand(IEmployeeService empSrv)
        {
            this.empSrv = empSrv;
        }

        public string Execute(string[] data)
        {
            var employeeId = int.Parse(data[0]);
            var managerId = int.Parse(data[1]);

            var empPI = this.empSrv.EmployeeManagerById(employeeId);

            empPI.ManagerId = managerId;

            this.empSrv.SetManager(empPI);

            return $"Manager with ID {managerId} successfully set to employee {empPI.FirstName} {empPI.LastName}";
        }
    }
}
