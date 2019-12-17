using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthwindWeb.Models;
using NorthwindWeb.Repository;
using NorthwindWeb.Services;
using NSubstitute;
using System.Collections.Generic;
using System.Linq;

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

            var customers = new List<Customer>
            {
                new Customer() {CustomerID = "ALFKI", ContactName = "Maria Anders"},
                new Customer() {CustomerID = "ANATR", ContactName = "Ana Trujillo"},
                new Customer() {CustomerID = "ANTON", ContactName = "Antonio Moreno"},
            };
            CustomersCount = customers.Count;

            _repository.LookupAll().Returns(customers.AsQueryable());
        }

        [TestMethod]
        public void get_all()
        {
            CustomersCountShouldBe(CustomersCount);
        }

        private void CustomersCountShouldBe(int expected)
        {
            var customers = _customersService.GetAll();

            Assert.AreEqual(expected, customers.Count);
        }
    }
}