using NorthwindWeb.Models;
using NorthwindWeb.Repository;
using System.Collections.Generic;
using System.Linq;

namespace NorthwindWeb.Services
{
    public class CustomersService
    {
        private IRepository<Customer> _repository;
        private IUnitOfWork _unitOfWork;

        public CustomersService(IUnitOfWork unitOfWork, IRepository<Customer> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public List<Customer> GetAll()
        {
            return _repository.LookupAll().ToList();
        }

        public Customer GetById(string id)
        {
            return _repository.GetSingle(x => x.CustomerID == id);
        }

        public void Create(Customer customer)
        {
            _repository.Create(customer);
            _unitOfWork.Commit();
        }

        public void Update(Customer customer)
        {
            _repository.Update(customer);
            _unitOfWork.Commit();
        }

        public void Delete(Customer customer)
        {
            _repository.Remove(customer);
            _unitOfWork.Commit();
        }
    }
}