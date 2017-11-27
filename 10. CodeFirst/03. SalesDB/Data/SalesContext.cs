using Microsoft.EntityFrameworkCore;
using P03_SalesDatabase.Data.Models;

namespace P03_SalesDatabase.Data
{
    public class SalesContext : DbContext
    {
        public SalesContext()
        { }

        public SalesContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Product> Product { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=Sales;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Sale>(e =>
            {
                e.HasOne(s => s.Customer)
                    .WithMany(c => c.Sales)
                    .HasForeignKey(s => s.CustomerId);

                e.HasOne(s => s.Product)
                    .WithMany(p => p.Sales)
                    .HasForeignKey(s => s.ProductId);

                e.HasOne(s => s.Store)
                    .WithMany(st => st.Sales)
                    .HasForeignKey(s => s.StoreId);

                e.Property(s => s.Date)
                    .HasDefaultValueSql("GETDATE()");

                e.HasKey(s => s.SaleId);
            });

            modelBuilder.Entity<Product>(e =>
            {
                e.Property(p => p.Name)
                    .HasMaxLength(50)
                    .IsUnicode(true);

                e.Property(p => p.Description)
                    .HasMaxLength(250)
                    .IsUnicode(true)
                    .HasDefaultValue("No description");

                e.HasKey(p => p.ProductId);
            });

            modelBuilder.Entity<Customer>(e =>
            {
                e.Property(c => c.Name)
                    .HasMaxLength(100)
                    .IsUnicode(true);
                e.Property(c => c.Email)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                e.HasKey(c => c.CustomerId);
            });

            modelBuilder.Entity<Store>(e =>
            {
                e.Property(s => s.Name)
                    .HasMaxLength(80)
                    .IsUnicode(true);

                e.HasKey(s => s.StoreId);
            });
        }
    }
}
