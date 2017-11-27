using Microsoft.EntityFrameworkCore;
using P01_StudentSystem.Data.Models;

namespace P01_StudentSystem.Data
{
    public partial class StudentSystemContext : DbContext
    {
        public StudentSystemContext()
        { }

        public StudentSystemContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Homework> HomeworkSubmissions { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=StudentSystem;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Course>(e =>
            {
                e.ToTable("Courses");

                e.Property(c => c.Name)
                    .HasMaxLength(80)
                    .IsUnicode(true);

                e.Property(c => c.Description)
                    .IsUnicode(true)
                    .IsRequired(false);

                e.HasKey(c => c.CourseId);
            });

            modelBuilder.Entity<Student>(e =>
            {
                e.ToTable("Students");

                e.Property(s => s.Name)
                    .HasMaxLength(100)
                    .IsUnicode(true);

                e.Property(s => s.PhoneNumber)
                    .IsUnicode(false)
                    .IsRequired(false);

                e.Property(s => s.Birthday)
                    .IsRequired(false);

                e.HasKey(s => s.StudentId);
            });

            modelBuilder.Entity<Resource>(e =>
            {
                e.ToTable("Resources");

                e.Property(r => r.Name)
                    .HasMaxLength(50)
                    .IsUnicode(true);

                e.Property(r => r.Url)
                    .IsUnicode(false);

                e.HasKey(r => r.ResourceId);

                e.HasOne(r => r.Course)
                    .WithMany(c => c.Resources)
                    .HasForeignKey(r => r.CourseId);
            });

            modelBuilder.Entity<Homework>(e =>
            {
                e.ToTable("HomeworkSubmissions");

                e.HasKey(h => h.HomeworkId);

                e.Property(h => h.Content)
                    .IsUnicode(false);

                e.HasOne(h => h.Student)
                    .WithMany(s => s.HomeworkSubmissions)
                    .HasForeignKey(h => h.StudentId);

                e.HasOne(h => h.Course)
                    .WithMany(s => s.HomeworkSubmissions)
                    .HasForeignKey(h => h.CourseId);
            });

            modelBuilder.Entity<StudentCourse>(e =>
            {
                e.ToTable("StudentCourses");

                e.HasKey(h => new { h.StudentId, h.CourseId });

                e.HasOne(h => h.Student)
                    .WithMany(s => s.CourseEnrollments)
                    .HasForeignKey(h => h.StudentId);

                e.HasOne(h => h.Course)
                    .WithMany(s => s.StudentsEnrolled)
                    .HasForeignKey(h => h.CourseId);
            });

        }
    }
}
