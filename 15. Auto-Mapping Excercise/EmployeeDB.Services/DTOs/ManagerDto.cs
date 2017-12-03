using EmployeeDB.Client.DTOs;
using System.Collections.Generic;
using System.Text;

namespace EmployeeDB.Services.DTOs
{
    public class ManagerDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<EmployeeDto> EmployeesManaged { get; set; }
        public int EmployeesManagedCount { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"{FirstName} {LastName} | Employees: {EmployeesManagedCount}");

            if (EmployeesManagedCount != 0)
            {
                foreach (var employee in EmployeesManaged)
                {
                    sb.AppendLine($"    - {employee.ToString()}");
                }
            }

            return sb.ToString().Trim();
        }
    }
}
