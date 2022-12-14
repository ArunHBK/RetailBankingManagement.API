using BankManagementSystem.Data;
using BankManagementSystem.Models;
using BankManagementSystem.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace BankManagementSystem.UnitTests.RepositoryTests
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class TransactionRepoInMemoryTest
    {
            private static DbContextOptions<DataContext> dbContextOptions = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "BankDb")
            .Options;

            DataContext context;
            TransactionRepo transactionRepo;

            [OneTimeSetUp]
            public void Setup()
            {
                context = new DataContext(dbContextOptions);
                context.Database.EnsureCreated();

                SeedDatabase();

                transactionRepo = new TransactionRepo(context);
            }
            [OneTimeTearDown]
            public void CleanUp()
            {
                context.Database.EnsureDeleted();
            }

            private void SeedDatabase()
            {


                var transactions = new List<Transaction>()
            {
                new Transaction()
                {
                   Id = 1, TransactionType = "Deposit", Amount = 58, ClosingBalance =578, Username ="Vaish"
                },
                new Transaction()
                {
                    Id = 2, TransactionType = "Withdraw", Amount = 78, ClosingBalance =6889, Username ="Ram"
                },
                new Transaction()
                {
                   Id = 3, TransactionType = "Withdraw", Amount = 96, ClosingBalance =3258, Username ="Vaish"
                },
            };
                context.Transactions.AddRange(transactions);


                context.SaveChanges();
            }

            [Test]
            public async Task GetTransactionByUsername_ReturnTrue_Test()
            {
                TransactionRepo transactionRepo = new TransactionRepo(context);
                ResponseObject transaction = await transactionRepo.ViewByUsername("Vaish");

                Assert.IsTrue(transaction.Status);

            }
            [Test]
            public async Task GetTransactionByUsername_ReturnFalse_Test()
            {
                TransactionRepo transactionRepo = new TransactionRepo(context);
                ResponseObject transaction = await transactionRepo.ViewByUsername("Jayarath");

                Assert.IsFalse(transaction.Status);
            }
    }
}
