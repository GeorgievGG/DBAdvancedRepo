using Instagraph.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Instagraph.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.HasIndex(u => u.Username)
                .IsUnique();

            builder.Property(u => u.Username)
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(u => u.Password)
                .HasMaxLength(20)
                .IsRequired();

            builder.HasOne(u => u.ProfilePicture)
                .WithMany(p => p.Users)
                .HasForeignKey(u => u.ProfilePictureId);
        }
    }
}
