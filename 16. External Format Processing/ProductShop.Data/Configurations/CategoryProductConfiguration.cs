using Microsoft.EntityFrameworkCore;
using ProductShop.Models;

namespace ProductShop.Data.Configurations
{
    public class CategoryProductConfiguration : IEntityTypeConfiguration<CategoryProduct>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<CategoryProduct> builder)
        {
            builder.HasKey(cp => new { cp.ProductId, cp.CategoryId });
            builder.HasOne(cp => cp.Product)
                .WithMany(p => p.Categories)
                .HasForeignKey(cp => cp.ProductId);
            builder.HasOne(cp => cp.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(cp => cp.CategoryId);
        }
    }
}
