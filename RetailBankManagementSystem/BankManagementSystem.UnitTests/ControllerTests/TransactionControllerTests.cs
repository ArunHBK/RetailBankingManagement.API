using BankManagementSystem.Controllers;
using BankManagementSystem.Data;
using BankManagementSystem.Models;
using BankManagementSystem.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace BankManagementSystem.UnitTests.Controllers
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class TransactionControllerTests
    {
        //private static DbContextOptions<DataContext> dbContextOptions= new DbContextOptionsBuilder<DataContext>()
        // .UseInMemoryDatabase(databaseName : "TransactionDbTest")   
        //    .Options;

        //DataContext context;
        //[SetUp]
        //public void Setup()
        //{
        //}
        // [Test]
        //public void Test1()
        //{
        //    var mockDataContext = new Mock<IDataContext>();

        //    var transactionRepo = new TransactionRepo(mockDataContext.Object);
        //}
        [Test]
        public async Task GetTransactionDetailsByUserName_ReturnOK_Test()
        {
            var mockTransactionRepo = new Mock<ITransactionRepo>();
            mockTransactionRepo.Setup(x => x.ViewByUsername("Allen"))
           .Returns(Task.FromResult(new ResponseObject { Status= true }));

            var transactionController = new TransactionController(mockTransactionRepo.Object);
            var result = await transactionController.GetTransactionDetailsByUsername("Allen");
            //Assert.AreEqual(200, result.StatusCode);
            //var result = transactionController.GetUserByUsername("Vaish");
            // var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.NotNull(result);
        }

        [Test]
        public async Task GetTransactionDetailsByUserName_ReturnBadRequest_Test()
        {
            var mockTransactionRepo = new Mock<ITransactionRepo>();
            mockTransactionRepo.Setup(x => x.ViewByUsername("Vaish"))
           .Returns(Task.FromResult(new ResponseObject { Status= false }));

            var transactionController = new TransactionController(mockTransactionRepo.Object);
            var result = await transactionController.GetTransactionDetailsByUsername("Vaish");
            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
            Assert.NotNull(result);
        }
    }
}
