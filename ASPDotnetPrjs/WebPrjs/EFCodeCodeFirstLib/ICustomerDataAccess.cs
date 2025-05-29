using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCodeCodeFirstLib
{
    public interface ICustomerDataAccess
    {
        List<Customer> GetCustomers();
        void AddCustomer(Customer customer);
    }
}
