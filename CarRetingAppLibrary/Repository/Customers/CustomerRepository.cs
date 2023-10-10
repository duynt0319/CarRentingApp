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
        CustomerDAO cusDAO = new CustomerDAO();
        public Customer CheckCustomer(string email, string password)
        {
            return cusDAO.GetCustomerByEmailAndPassword(email, password);
        }

        public void CreateCustomer(Customer c)
        {
            cusDAO.CreateCustomer(c);
        }

        public void DeleteCustomer(Customer c)
        {
            cusDAO.DeleteCustomer(c);
        }

        public Customer GetCustomerById(int? id)
        {
           return cusDAO.GetCustomerById(id);
        }

        public List<Customer> GetCustomers()
        {
           return cusDAO.GetCustomers();
        }

        public void UpdateCustomer(Customer c)
        {
            cusDAO.UpdateCustomer(c);
        }
    }
}
