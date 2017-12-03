using EmployeeDB.Client.Commands.Contracts;
using EmployeeDB.Services.Contracts;
using System;
using System.Globalization;

namespace EmployeeDB.Client.Commands
{
    public class SetBirthdayCommand : ICommand
    {
        private readonly IEmployeeService empSrv;

        public SetBirthdayCommand(IEmployeeService empSrv)
        {
            this.empSrv = empSrv;
        }

        public string Execute(string[] data)
        {
            var employeeId = int.Parse(data[0]);
            var birthday = DateTime.ParseExact(data[1], "dd-MM-yyyy", CultureInfo.InvariantCulture);

            var empPI = this.empSrv.PersonalInfoByID(employeeId);

            empPI.Birthday = birthday;

            this.empSrv.UpdateEmployee(empPI);

            return $"Birthday {birthday} successfully set to employee {empPI.FirstName} {empPI.LastName}";
        }
    }
}
