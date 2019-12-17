using NorthwindWeb.Controllers;
using NorthwindWeb.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using NorthwindWeb.Repository;

namespace NorthwindWeb.Services
{
    public class CustomersService
    {
        //private NorthwindDb db = new NorthwindDb();
        private Repository<Customer> _repository;
        private UnitOfWork _unitOfWork;

        public CustomersService()
        {
            _unitOfWork = new UnitOfWork();
            _repository = new Repository<Customer>(_unitOfWork);
        }

        public List<Customer> GetAll()
        {
            //return db.Customers.ToList();
            return _repository.LookupAll().ToList();
        }

        public Customer GetById(string id)
        {
            //return db.Customers.Find(id);
            return _repository.GetSingle(x=>x.CustomerID == id);
        }

        public void Create(Customer customer)
        {
            //db.Customers.Add(customer);
            //db.SaveChanges();
            _repository.Create(customer);
            _unitOfWork.Commit();
        }

        public void Update(Customer customer)
        {
            //db.Entry(customer).State = EntityState.Modified;
            //db.SaveChanges();
            _repository.Update(customer);
            _unitOfWork.Commit();
        }

        public void Delete(Customer customer)
        {
            //db.Customers.Remove(customer);
            //db.SaveChanges();
            _repository.Remove(customer);
           _unitOfWork.Commit(); 
        }
    }
}