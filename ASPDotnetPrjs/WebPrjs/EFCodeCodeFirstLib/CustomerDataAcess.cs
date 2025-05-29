using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCodeCodeFirstLib
{
    public class CustomerDataAcess : ICustomerDataAccess
    {
        private readonly CustomerDbContext dbCtx;
        public CustomerDataAcess(CustomerDbContext _dbCtx)
        {
            this.dbCtx = _dbCtx;
        }
        public void AddCustomer(Customer customer)
        {
            dbCtx.tbl_customers.Add(customer);
            dbCtx.SaveChanges();
        }

        public List<Customer> GetCustomers()
        {
            return dbCtx.tbl_customers.ToList();
        }
    }
}
