using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_BillsPaymentSystem.Data.Models;

namespace P01_BillsPaymentSystem.Data.Configurations
{
    public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.Property(ba => ba.BankName)
                    .IsRequired(true)
                    .HasMaxLength(50)
                    .IsUnicode(true);

            builder.Property(ba => ba.SwiftCode)
                .IsRequired(true)
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.HasKey(ba => ba.BankAccountId);
        }
    }
}
