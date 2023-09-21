using CarRetingAppLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRetingAppLibrary.Repository.Customers
{
    public interface ICustomerRepository
    {
        void DeleteCustomer(Customer c);
        void UpdateCustomer(Customer c);
        void CreateCustomer(Customer c);
        List<Customer> GetCustomers();
    }
}
