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
        private static FUCarRentingManagementContext context;

        static CustomerDAO()
        {
            context = new FUCarRentingManagementContext();
        }

        public static List<Customer> GetCustomers()
        {
            return context.Customers.ToList();
        }

        public static void CreateCustomer(Customer customer)
        {
            context.Customers.Add(customer);
            context.SaveChanges();
        }

        public static void UpdateCustomer(Customer customer)
        {
            context.Entry(customer).State = EntityState.Modified;
            context.SaveChanges();
        }

        public static void DeleteCustomer(Customer customer)
        {
            var customerToDelete = context.Customers.SingleOrDefault(c => c.CustomerId == customer.CustomerId);
            if (customerToDelete != null)
            {
                context.Customers.Remove(customerToDelete);
                context.SaveChanges();
            }
        }

    }
}
