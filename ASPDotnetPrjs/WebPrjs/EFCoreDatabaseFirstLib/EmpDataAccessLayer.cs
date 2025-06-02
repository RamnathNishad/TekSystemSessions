using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreDatabaseFirstLib
{
    public class EmpDataAccessLayer : IEmpDataAccess
    {
        private readonly EmployeeDbContext dbCtx;
        public EmpDataAccessLayer(EmployeeDbContext _dbCtx)
        {
            this.dbCtx = _dbCtx;
        }

        public void AddEmployee(Employee emp)
        {
            dbCtx.tbl_employee.Add(emp);
            dbCtx.SaveChanges();
        }

        public void DeleteEmpById(int id)
        {
            var emp = dbCtx.tbl_employee.Find(id);
            dbCtx.tbl_employee.Remove(emp);
            dbCtx.SaveChanges();
        }

        public Employee GetEmpById(int id)
        {
            var emp = dbCtx.tbl_employee.Find(id);
            return emp;
        }

        public List<Employee> GetEmps()
        {
            return dbCtx.tbl_employee.ToList();
        }

        public bool Login(UserDetails userDetails)
        {
            var user = dbCtx.tbl_user
                            .Where(o => o.UserName == userDetails.UserName && o.Password == userDetails.Password)
                            .SingleOrDefault();
            if (user == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void UpdateEmp(Employee emp)
        {
            var record = dbCtx.tbl_employee.Find(emp.Ecode);
            //update the values
            record.Ename = emp.Ename;
            record.Salary = emp.Salary;
            record.Deptid=emp.Deptid;
            //save the changes
            dbCtx.SaveChanges();
        }
    }
}
