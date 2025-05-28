using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreDatabaseFirstLib
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Ecode { get; set; }
        public string Ename { get; set; }
        public int Salary {  get; set; }
        public int Deptid {  get; set; }
    }
}
