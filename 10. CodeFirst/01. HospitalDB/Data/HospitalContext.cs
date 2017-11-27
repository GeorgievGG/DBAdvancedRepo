
using Microsoft.EntityFrameworkCore;
using P01_HospitalDatabase.Data.Models;

namespace P01_HospitalDatabase.Data
{
    public class HospitalContext : DbContext
    {
        public HospitalContext()
        {

        }

        public HospitalContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Diagnose> Diagnoses { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<PatientMedicament> Prescriptions { get; set; }
        public DbSet<Visitation> Visitations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Diagnose>(entity =>
            {
                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Diagnoses)
                    .HasForeignKey(d => d.PatientId);

                entity.Property(d => d.Comments)
                    .HasMaxLength(250)
                    .IsUnicode(true);
                entity.Property(d => d.Name)
                    .HasMaxLength(50)
                    .IsUnicode(true);

                entity.HasKey(d => d.DiagnoseId);
            });

            modelBuilder.Entity<PatientMedicament>(entity =>
            {
                entity.HasOne(pm => pm.Medicament)
                    .WithMany(m => m.Prescriptions)
                    .HasForeignKey(pm => pm.MedicamentId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(pm => pm.Patient)
                    .WithMany(p => p.Prescriptions)
                    .HasForeignKey(pm => pm.PatientId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasKey(pm => new { pm.PatientId, pm.MedicamentId });
            });

            modelBuilder.Entity<Visitation>(entity =>
            {
                entity.HasOne(v => v.Patient)
                    .WithMany(p => p.Visitations)
                    .HasForeignKey(v => v.PatientId);

                entity.HasOne(v => v.Doctor)
                    .WithMany(d => d.Visitations)
                    .HasForeignKey(v => v.DoctorId);

                entity.Property(v => v.Comments)
                    .HasMaxLength(250)
                    .IsUnicode(true);

                entity.HasKey(v => v.VisitationId);
            });

            modelBuilder.Entity<Medicament>(entity =>
            {
                entity.Property(m => m.Name)
                    .HasMaxLength(50)
                    .IsUnicode(true);

                entity.HasKey(m => m.MedicamentId);
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.Property(p => p.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(true);
                entity.Property(p => p.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(true);
                entity.Property(p => p.Address)
                    .HasMaxLength(250)
                    .IsUnicode(true);
                entity.Property(p => p.Email)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.HasKey(p => p.PatientId);
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.Property(d => d.Name)
                    .HasMaxLength(100)
                    .IsUnicode(true);
                entity.Property(d => d.Specialty)
                    .HasMaxLength(100)
                    .IsUnicode(true);

                entity.HasKey(d => d.DoctorId);
            });
        }
    }
}
