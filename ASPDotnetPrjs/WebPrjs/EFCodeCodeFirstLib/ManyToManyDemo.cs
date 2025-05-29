using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCodeCodeFirstLib
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Enrollment> Enrollments { get; set; }
    }

    public class Enrollment
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }

        public DateTime EnrollmentDate { get; set; }
    }

    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<Enrollment> Enrollments { get; set; }
    }

}
