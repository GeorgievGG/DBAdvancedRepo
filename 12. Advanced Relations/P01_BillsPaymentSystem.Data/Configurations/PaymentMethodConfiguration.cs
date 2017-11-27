using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_BillsPaymentSystem.Data.Models;

namespace P01_BillsPaymentSystem.Data.Configurations
{
    public class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.HasOne(pm => pm.User)
                .WithMany(u => u.PaymentMethods)
                .HasForeignKey(pm => pm.UserId);

            builder.HasOne(pm => pm.CreditCard)
                .WithMany()
                .HasForeignKey(pm => pm.CreditCardId);

            builder.HasOne(pm => pm.BankAccount)
                .WithMany()
                .HasForeignKey(pm => pm.BankAccountId);

            builder.HasKey(pm => pm.Id);

            builder.HasIndex(pm => new { pm.UserId, pm.BankAccountId }).IsUnique(true);
            builder.HasIndex(pm => new { pm.UserId, pm.CreditCardId }).IsUnique(true);
        }
    }
}
