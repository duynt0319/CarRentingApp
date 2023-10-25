using CarRetingAppLibrary.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRetingAppLibrary.BussinessObject
{
    public class CustomerDAO
    {
        private FUCarRentingManagementContext context;
        public CustomerDAO()
        {
            context = new FUCarRentingManagementContext();
        }
        public List<Customer> GetCustomers()
        {
            return context.Customers.ToList();
        }

        public void CreateCustomer(Customer customer)
        {
            context.Customers.Add(customer);
            context.SaveChanges();
        }

        public void UpdateCustomer(Customer customer)
        {
            Customer cus = GetCustomerById(customer.CustomerId);
            if(cus != null)
            {
                context.Entry(customer).State = EntityState.Modified;
                context.SaveChanges();
            }     
        }

        public void DeleteCustomer(Customer customer)
        {
            var customerToDelete = context.Customers.SingleOrDefault(c => c.CustomerId == customer.CustomerId);
            if (customerToDelete != null)
            {
                context.Customers.Remove(customerToDelete);
                context.SaveChanges();
            }
        }


        public Customer GetCustomerByEmailAndPassword(string email, string password)
        {
            try
            {
                var customer = context.Customers
                    .FirstOrDefault(c => c.Email == email && c.Password == password);
                return customer;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public Customer GetCustomerById(int? id)
        {
            try
            {
                var customer = context.Customers.AsNoTracking()
                    .FirstOrDefault(c => c.CustomerId == id);
                return customer;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<Customer> GetCustomerListIQ()
        {
            IQueryable<Customer> customers;
            try
            {
                var carRentingManagementDB = new FUCarRentingManagementContext();
                customers = carRentingManagementDB.Customers;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return customers;
        }
    }
}
