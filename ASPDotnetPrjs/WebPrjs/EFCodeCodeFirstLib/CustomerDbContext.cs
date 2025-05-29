using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCodeCodeFirstLib
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<Customer> tbl_customers { get; set; }
        public DbSet<User> tbl_users { get; set; }
        public DbSet<UserProfile> tbl_profiles { get; set; }
    
        public DbSet<Student> tbl_students { get; set; }
        public DbSet<Course> tbl_courses { get; set; }
        public DbSet<Enrollment> tbl_enrollments { get; set; }

    }
}
