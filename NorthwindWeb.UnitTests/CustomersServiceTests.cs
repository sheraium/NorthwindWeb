using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthwindWeb.Models;
using NorthwindWeb.Repository;
using NorthwindWeb.Services;
using NSubstitute;

namespace NorthwindWeb.UnitTests
{
    [TestClass]
    public class CustomersServiceTests
    {
        private CustomersService _customersService;
        private IRepository<Customer> _repository;
        private IUnitOfWork _unitOfWork;
        private int CustomersCount;

        [TestInitialize]
        public void SetUp()
        {
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _repository = Substitute.For<IRepository<Customer>>();
            _customersService = new CustomersService(_unitOfWork, _repository);
        }

        [TestMethod]
        public void get_all()
        {
            GivenAllCustomers();
            CustomersCountShouldBe(CustomersCount);
        }

        [TestMethod]
        public void get_by_id()
        {
            GivenOneCustomer(new Customer() {CustomerID = "ANATR", ContactName = "Ana Trujillo"});
            CustomerIdShould("ANATR");
        }

        [TestMethod]
        public void create()
        {
            var customer = new Customer() {CustomerID = "PETER"};
            CreateCustomer(customer);
            NewCustomerIdShouldBe(customer.CustomerID);
        }

        private void NewCustomerIdShouldBe(string expected)
        {
            _repository.Received().Create(Arg.Is<Customer>(c => c.CustomerID == expected));
        }

        private void CreateCustomer(Customer customer)
        {
            _customersService.Create(customer);
        }

        private void CustomerIdShould(string customerId)
        {
            var customer = _customersService.GetById(customerId);

            Assert.AreEqual(customerId, customer.CustomerID);
        }

        private void GivenOneCustomer(Customer customer)
        {
            _repository.GetSingle(Arg.Any<Expression<Func<Customer, bool>>>())
                .Returns(customer);
        }

        private void GivenAllCustomers()
        {
            var customers = new List<Customer>
            {
                new Customer() {CustomerID = "ALFKI", ContactName = "Maria Anders"},
                new Customer() {CustomerID = "ANATR", ContactName = "Ana Trujillo"},
                new Customer() {CustomerID = "ANTON", ContactName = "Antonio Moreno"},
            };
            CustomersCount = customers.Count;

            _repository.LookupAll().Returns(customers.AsQueryable());
        }

        private void CustomersCountShouldBe(int expected)
        {
            var customers = _customersService.GetAll();

            Assert.AreEqual(expected, customers.Count);
        }
    }
}