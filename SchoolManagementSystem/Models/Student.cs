using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
      
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PasswordHash { get; set; }


        // Navigation propertys
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}