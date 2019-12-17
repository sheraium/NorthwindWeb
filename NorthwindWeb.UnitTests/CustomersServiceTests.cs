using System.Collections.Generic;
using System.Linq;
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
        [TestMethod]
        public void get_all()
        {
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var repository = Substitute.For<IRepository<Customer>>();
            var customersService = new CustomersService(unitOfWork, repository);
            repository.LookupAll().Returns(new List<Customer>
            {
                new Customer() {CustomerID = "ALFKI", ContactName = "Maria Anders"},
                new Customer() {CustomerID = "ANATR", ContactName = "Ana Trujillo"},
                new Customer() {CustomerID = "ANTON", ContactName = "Antonio Moreno"},
            }.AsQueryable());

            var customers = customersService.GetAll();

            Assert.AreEqual(3, customers.Count);
        }
    }
}