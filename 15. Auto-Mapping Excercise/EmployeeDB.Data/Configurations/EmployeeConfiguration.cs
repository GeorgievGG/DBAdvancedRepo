using EmployeeDB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeDB.Data.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(p => p.Birthday)
                    .IsRequired(false);
            builder.Property(p => p.Address)
                    .IsRequired(false);
            builder.Property(p => p.ManagerId)
                    .IsRequired(false);
            builder.HasOne(e => e.Manager)
                .WithMany(m => m.EmployeesManaged)
                .HasForeignKey(e => e.ManagerId);
        }
    }
}
