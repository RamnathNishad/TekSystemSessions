using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreDatabaseFirstLib
{
    public interface IEmpDataAccess
    {
        List<Employee> GetEmps();
        Employee GetEmpById(int id);
        void AddEmployee(Employee emp);
        void DeleteEmpById(int id);
        void UpdateEmp(Employee emp);
    }
}
