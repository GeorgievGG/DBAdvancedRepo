using EmployeeDB.Client.Commands.Contracts;
using EmployeeDB.Services.Contracts;
using System.Linq;

namespace EmployeeDB.Client.Commands
{
    public class SetAddressCommand : ICommand
    {
        private readonly IEmployeeService empSrv;

        public SetAddressCommand(IEmployeeService empSrv)
        {
            this.empSrv = empSrv;
        }

        public string Execute(string[] data)
        {
            var employeeId = int.Parse(data[0]);
            var address = string.Join(" ", data.Skip(1).ToArray());

            var empPI = this.empSrv.PersonalInfoByID(employeeId);

            empPI.Address = address;

            this.empSrv.UpdateEmployee(empPI);

            return $"Address {address} successfully set to employee {empPI.FirstName} {empPI.LastName}";
        }
    }
}
