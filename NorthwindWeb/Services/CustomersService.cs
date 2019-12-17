﻿using NorthwindWeb.Models;
using NorthwindWeb.Repository;
using System.Collections.Generic;
using System.Linq;

namespace NorthwindWeb.Services
{
    public class CustomersService
    {
        private Repository<Customer> _repository;
        private UnitOfWork _unitOfWork;

        public CustomersService()
        {
            _unitOfWork = new UnitOfWork();
            _repository = new Repository<Customer>(_unitOfWork);
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