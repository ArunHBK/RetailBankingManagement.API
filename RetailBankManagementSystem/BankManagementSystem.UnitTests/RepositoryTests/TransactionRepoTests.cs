using BankManagementSystem.Controllers;
using BankManagementSystem.Data;
using BankManagementSystem.Models;
using BankManagementSystem.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BankManagementSystem.UnitTests.RepositoryTest
{
    [ExcludeFromCodeCoverage]
    [TestFixture]

    public class TransactionRepoTests
    {
        //[Test]
        //public async Task Test()
        //{
        //    var mockContext = new Mock<DataContext>();
        //    Mock<ITransactionRepo> mockTransactionRepo = new Mock<ITransactionRepo>();
        //    Mock<DbContext> mockDbContext = new Mock<DbContext>();
        //    var mockTransactionRepo = new Mock<DbSet<Transaction>>();
        //    mockContext.Setup(x => x.)
        //   .Returns(Task.FromResult(new ResponseObject { Status= false }));

        // var transactionRepo = new TransactionRepo(mockTransactionRepo.Object);
        // var result = await transactionRepo.ViewByUsername("Vaish");
        // Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        // Assert.NotNull(result);.

        //       var mockSet = new Mock<DbSet<Transaction>>();

        //       var mockContext = new Mock<DataContext>();
        //       mockContext.Setup(m => m.Transactions)
        //       .Returns(mockSet.Object);

        //       var transactionRepo = new TransactionRepo(mockContext.Object);
        //       transactionRepo.ViewByUsername("Vaish");

        //       mockSet.Verify(m => m.Add(It.IsAny<Transaction>()), Times.Once());
        //       mockContext.Verify(m => m.SaveChanges(), Times.Once());
        //}
        //[Test]
        //public async Task GetAllBlogs_orders_by_name()
        //{
        //    var data = new List<Transaction>
        //    {
        //        new Transaction { Username = "BBB" },
        //        new Transaction { Username = "ZZZ" },
        //        new Transaction { Username = "AAA" },
        //    }.AsQueryable();

        //    var mockSet = new Mock<DbSet<Transaction>>();
        //    mockSet.As<IDbAsyncEnumerable<Transaction>>()
        //        .Setup(m => m.GetAsyncEnumerator())
        //        .Returns(new TestDbAsyncEnumerator<Transaction>(data.GetEnumerator()));

        //    mockSet.As<IQueryable<Transaction>>()
        //        .Setup(m => m.Provider)
        //        .Returns(new TestDbAsyncQueryProvider<Transaction>(data.Provider));

        //    mockSet.As<IQueryable<Transaction>>().Setup(m => m.Expression).Returns(data.Expression);
        //    mockSet.As<IQueryable<Transaction>>().Setup(m => m.ElementType).Returns(data.ElementType);
        //    mockSet.As<IQueryable<Transaction>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

        //    var mockContext = new Mock<DataContext>();
        //    mockContext.Setup(c => c.Transactions).Returns(mockSet.Object);

        //    var service = new TransactionRepo(mockContext.Object);
        //    var Transactions = await service.ViewByUsername("Vaish");

        //    Assert.AreEqual(3, Transactions.Count);
        //    Assert.AreEqual("AAA", Transactions[0].Username);
        //    Assert.AreEqual("BBB", Transactions[1].Username);
        //    Assert.AreEqual("ZZZ", Transactions[2].Username);
        //}
        /* [Test]
         public async Task GetAllBlogs_orders_by_name()
         {

           var data = new List<Transaction>
             {
                 new Transaction { Username = "BBB" },
                 new Transaction { Username = "ZZZ" },
                 new Transaction { Username = "AAA" },
             }.AsQueryable();

             var optionsmock = new Mock<DbContextOptions<DataContext>>();

             var mockSet = new Mock<DbSet<Transaction>>();
             mockSet.As<IDbAsyncEnumerable<Transaction>>()
                 .Setup(m => m.GetAsyncEnumerator())
                 .Returns(new DataContext(optionsmock.Object));
             mockSet.As<IQueryable<Transaction>>().Setup(m => m.Provider).Returns(new DataContext (data.Provider));
             mockSet.As<IQueryable<Transaction>>().Setup(m => m.Expression).Returns(data.Expression);
             mockSet.As<IQueryable<Transaction>>().Setup(m => m.ElementType).Returns(data.ElementType);
             mockSet.As<IQueryable<Transaction>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

             var mockContext = new Mock<DataContext>();
             mockContext.Setup(x=>x.Transactions)
                 .Returns(mockSet.Object);

             var service = new TransactionRepo(mockContext.Object);
             var transactions = await service.ViewByUsername("Vaish");

             Assert.AreEqual(true, transactions.Value);
             //Assert.AreEqual("AAA", transactions[0].Name);
             //Assert.AreEqual("BBB", transactions[1].Name);
             //Assert.AreEqual("ZZZ", transactions[2].Name);
         }*/
        
       
       // private Mock<DataContext> _mockContext;
       // [Test]
       // public async Task GetAllBlogs_orders_by_name()
       // {
       //     var data = new List<Transaction>
       //      {
       //          new Transaction { Username = "BBB" },
       //          new Transaction { Username = "ZZZ" },
       //          new Transaction { Username = "AAA" },
       //      }.AsQueryable();

       //     var optionsmock = new Mock<DbContextOptions<DataContext>>();

       //     var mockSet = new Mock<DbSet<Transaction>>();
       //     mockSet.As<IAsyncEnumerable<Transaction>>()
       //.Setup(m => m.GetAsyncEnumerator())
       //.Returns(new TestAsyncEnumerator<Transaction>(data.GetEnumerator()));

       //     mockSet.As<IQueryable<Transaction>>()
       //         .Setup(m => m.Provider)
       //         .Returns(new Test9AsyncQueryProvider<Transaction>(data.Provider));

       //     //mockSet.As<IQueryable<Transaction>>().Setup(m => m.Provider).Returns(data.Provider);
       //     mockSet.As<IQueryable<Transaction>>().Setup(m => m.Expression).Returns(data.Expression);
       //     mockSet.As<IQueryable<Transaction>>().Setup(m => m.ElementType).Returns(data.ElementType);
       //     mockSet.As<IQueryable<Transaction>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

       //     _mockContext = new Mock<DataContext>();
       //     _mockContext.Setup(c => c.Transactions)
       //         .Returns(mockSet.Object);

       //     var service = new TransactionRepo(_mockContext.Object);
       //     var transactions = await service.ViewByUsername("Vaish");

       //     Assert.AreEqual(true, transactions.Value);
       //     //Assert.AreEqual("AAA", transactions[0].Name);
       //     //Assert.AreEqual("BBB", transactions[1].Name);
       //     //Assert.AreEqual("ZZZ", transactions[2].Name);
       // }
    }
}
