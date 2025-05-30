using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqDemos
{
    internal class Department
    {
        public int Deptid {  get; set; }
        public string Dname { get; set; }
        public int Dhead { get; set; }

        public static List<Department> Departments { get; set; } = new List<Department>
        { 
            new Department{Deptid=201,Dname="Account",Dhead=105 },
            new Department{Deptid=202,Dname="Admin",Dhead=108 },
            new Department{Deptid=203,Dname="Sales",Dhead=104 }
        };
    }
}
