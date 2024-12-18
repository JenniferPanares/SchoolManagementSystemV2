namespace SchoolManagementSystem.Data
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using SchoolManagementSystem.Models;

    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets for your entities
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<TuitionPayment> TuitionPayments { get; set; }
        public DbSet<TuitionBilling> TuitionBillings { get; set; }

        // Consolidated OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships for TuitionPayment and TuitionBilling
            modelBuilder.Entity<TuitionPayment>()
                .HasOne(tp => tp.TuitionBilling)
                .WithMany(tb => tb.TuitionPayments)
                .HasForeignKey(tp => tp.TuitionBillingId);

            // Configure primary key for Enrollment
            modelBuilder.Entity<Enrollment>()
                .HasKey(e => e.EnrollmentId);

            // Configure relationships for Enrollment
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.StudentId)
                .OnDelete(DeleteBehavior.SetNull); // Prevent cascade delete

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.SetNull); // Prevent cascade delete

            // Seed data for TuitionBilling and TuitionPayment
            modelBuilder.Entity<TuitionBilling>().HasData(
                new TuitionBilling { Id = 1, TotalAmount = 5000, BillingDate = DateTime.Now }
            );

            modelBuilder.Entity<TuitionPayment>().HasData(
                new TuitionPayment { Id = 1, AmountPaid = 2500, PaymentDate = DateTime.Now, TuitionBillingId = 1 }
            );

            // Seed data for Students
            var passwordHasher = new PasswordHasher<IdentityUser>();
            string password = "Test@123";

            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    StudentId = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@example.com",
                    Address = "123 Main Street",
                    DateOfBirth = new DateTime(2000, 1, 1),
                    PasswordHash = passwordHasher.HashPassword(null, password)
                },
                new Student
                {
                    StudentId = 2,
                    FirstName = "Jane",
                    LastName = "Smith",
                    Email = "jane.smith@example.com",
                    Address = "456 Elm Street",
                    DateOfBirth = new DateTime(1998, 5, 15),
                    PasswordHash = passwordHasher.HashPassword(null, password)
                }
            );

            // Seed data for Courses
            modelBuilder.Entity<Course>().HasData(
                new Course { CourseId = 1, CourseTitle = "Math 101", Description = "Basic Mathematics", Credits = 3 },
                new Course { CourseId = 2, CourseTitle = "Science 101", Description = "Introduction to Science", Credits = 4 },
                new Course { CourseId = 3, CourseTitle = "English 101", Description = "English Literature Basics", Credits = 3 });

        }
    }
}

