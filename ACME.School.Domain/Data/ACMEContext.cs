namespace ACME.School.Domain.Data
{
    using ACME.School.Domain.Entities;
    using Microsoft.EntityFrameworkCore;

    public class ACMEContext : DbContext
    {
        public ACMEContext() { }

        public ACMEContext(DbContextOptions<ACMEContext> options) : base(options) { }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Student> Students { get; set; }
    }
}
