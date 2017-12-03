using Microsoft.EntityFrameworkCore;
using ProductShop.Models;

namespace ProductShop.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.FirstName)
                .IsRequired(false);
            builder.Property(u => u.Age)
                .IsRequired(false);
        }
    }
}
