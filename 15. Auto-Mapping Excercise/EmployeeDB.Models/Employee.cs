using System;
using System.Collections.Generic;

namespace EmployeeDB.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Salary { get; set; }
        public DateTime? Birthday { get; set; }
        public string Address { get; set; }
        public int? Age
        {
            get
            {
                if (Birthday != null)
                {
                    DateTime zeroTime = new DateTime(1, 1, 1);
                    TimeSpan? timeDiff = (DateTime.Now - this.Birthday);
                    int curAge = (zeroTime + (TimeSpan)timeDiff).Year - 1;
                    return curAge;
                }
                else
                {
                    return null;
                }
            }
        }

        public int? ManagerId { get; set; }
        public Employee Manager { get; set; }

        public ICollection<Employee> EmployeesManaged { get; set; } = new List<Employee>();
    }
}
