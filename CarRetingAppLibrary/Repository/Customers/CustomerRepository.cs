using CarRetingAppLibrary.BussinessObject;
using CarRetingAppLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRetingAppLibrary.Repository.Customers
{
    public class CustomerRepository : ICustomerRepository
    {
        public void CreateCustomer(Customer c)
        {
            CustomerDAO.CreateCustomer(c);
        }

        public void DeleteCustomer(Customer c)
        {
           CustomerDAO.DeleteCustomer(c);
        }

        public List<Customer> GetCustomers()
        {
           return CustomerDAO.GetCustomers();
        }

        public void UpdateCustomer(Customer c)
        {
            CustomerDAO.UpdateCustomer(c);
        }
    }
}
