using EmployeeDB.Client.DTOs;

namespace EmployeeDB.Services.DTOs
{
    public class EmployeeManagerDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Salary { get; set; }
        public int? ManagerId { get; set; }
        public EmployeeDto Manager { get; set; }

        public override string ToString()
        {
            var managerName = (Manager != null) ? Manager.LastName : "[no manager]";
            return $"{FirstName} {LastName} - ${Salary:f2} - Manager: {managerName}";
        }
    }
}
