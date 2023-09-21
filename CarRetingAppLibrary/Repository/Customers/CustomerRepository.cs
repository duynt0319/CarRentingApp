using CarRetingSystemLibrary.BussinessObject;
using CarRetingSystemLibrary.DataAccess;
using CarRetingSystemLibrary.Repository.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRetingSystemLibrary.Repository.Customers
{
    public class CustomerRepository : ICustomerRepository
    {
        public void DeleteCustomer(Customer c) => CustomerDAO.DeleteCustomers(c);
        public void SaveCustomer(Customer c) => CustomerDAO.SaveCustomers(c);
        public void UpdateCustomer(Customer c) => CustomerDAO.UpdateCustomers(c);
        public List<Customer> GetCustomers() => CustomerDAO.GetCustomers();
    }
}
