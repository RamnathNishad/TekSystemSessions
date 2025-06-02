using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreDatabaseFirstLib
{
    public  class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) 
            : base(options)
        {
            
        }

        public DbSet<Employee> tbl_employee { get; set; }
        public DbSet<UserDetails> tbl_user { get; set; }
    }
}
