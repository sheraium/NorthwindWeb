using NorthwindWeb.Controllers;
using NorthwindWeb.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace NorthwindWeb.Services
{
    public class CustomersService
    {
        private NorthwindDb db = new NorthwindDb();

        public CustomersService()
        {
        }

        public List<Customer> GetAll()
        {
            return db.Customers.ToList();
        }

        public Customer GetById(string id)
        {
            return db.Customers.Find(id);
        }

        public void Create(Customer customer)
        {
            db.Customers.Add(customer);
            db.SaveChanges();
        }

        public void Update(Customer customer)
        {
            db.Entry(customer).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(Customer customer)
        {
            db.Customers.Remove(customer);
            db.SaveChanges();
        }
    }
}