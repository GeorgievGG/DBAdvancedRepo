using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_BillsPaymentSystem.Data.Models;

namespace P01_BillsPaymentSystem.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(true)
                    .IsRequired(true);

            builder.Property(u => u.LastName)
                .HasMaxLength(50)
                .IsUnicode(true)
                .IsRequired(true);

            builder.Property(u => u.Email)
                .HasMaxLength(80)
                .IsUnicode(false)
                .IsRequired(true);

            builder.Property(u => u.Password)
                .HasMaxLength(25)
                .IsUnicode(false)
                .IsRequired(true);

            builder.HasKey(u => u.UserId);
        }
    }
}
