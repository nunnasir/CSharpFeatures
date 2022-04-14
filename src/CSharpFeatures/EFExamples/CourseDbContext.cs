using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EFExamples
{
    public class CourseDbContext : DbContext
    {
        private readonly string _connectionString;
        private readonly string _assemblyName;

        public CourseDbContext()
        {
            _connectionString = "Server=DESKTOP-H28I08B;Database=csharpb9;User Id=csharpb9;Password=123456";
            _assemblyName = Assembly.GetExecutingAssembly().FullName;
        }

        public CourseDbContext(string connectionString, string assemblyName)
        {
            _connectionString = connectionString;
            _assemblyName = assemblyName;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString, m => m.MigrationsAssembly(_assemblyName));
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Fluent API
            //-------------------

            // Composite Key
            modelBuilder.Entity<CourseStudent>()
                .HasKey(cs => new { cs.StudentId, cs.CourseId});

            // Table Name Sets
            modelBuilder.Entity<Topic>().ToTable("Topics");

            // One To Many Relation
            // From Parent to Child
            modelBuilder.Entity<Course>()
                .HasMany(c => c.Topics)
                .WithOne(t => t.Course);

            // Many to Many Relation
            // From Middle Table
            modelBuilder.Entity<CourseStudent>()
                .HasOne(cs => cs.Course)
                .WithMany(c => c.CourseStudents)
                .HasForeignKey(cs => cs.CourseId);

            modelBuilder.Entity<CourseStudent>()
                .HasOne(cs => cs.Student)
                .WithMany(s => s.CourseStudents)
                .HasForeignKey(cs => cs.StudentId);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
    }
}
