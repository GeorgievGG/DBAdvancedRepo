using System;
using System.Globalization;
using System.Text;

namespace EmployeeDB.Client.DTOs
{
    public class PesonalInfoDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Salary { get; set; }
        public DateTime? Birthday { get; set; }
        public string Address { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"ID: {Id}. - {FirstName} {LastName} Salary: ${Salary:f2}");

            if (Birthday != null)
            {
                sb.AppendLine($"Birthday: {((DateTime)Birthday).ToString("dd-MM-yyyy", CultureInfo.InvariantCulture)}");
            }

            if (Address != null)
            {
                sb.AppendLine($"Address: {Address}");
            }

            return sb.ToString().Trim();
        }
    }
}
