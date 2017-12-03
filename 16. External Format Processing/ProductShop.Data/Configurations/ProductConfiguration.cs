using Microsoft.EntityFrameworkCore;
using ProductShop.Models;

namespace ProductShop.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Product> builder)
        {
            builder.Property(u => u.BuyerId)
                .IsRequired(false);
            builder.HasOne(u => u.Buyer)
                .WithMany(b => b.BoughtProducts)
                .HasForeignKey(u => u.BuyerId);
            builder.HasOne(u => u.Seller)
                .WithMany(s => s.ProductsForSale)
                .HasForeignKey(u => u.SellerId);
        }
    }
}
