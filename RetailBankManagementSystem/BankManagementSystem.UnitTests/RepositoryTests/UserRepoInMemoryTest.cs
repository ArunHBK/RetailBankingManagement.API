using BankManagementSystem.Data;
using BankManagementSystem.Models;
using BankManagementSystem.Models.DTO;
using BankManagementSystem.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagementSystem.UnitTests.RepositoryTests
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class UserRepoInMemoryTest
    {
        Mock<IConfiguration> configuration = new Mock<IConfiguration>();

        private static DbContextOptions<DataContext> dbContextOptions = new DbContextOptionsBuilder<DataContext>()
           .UseInMemoryDatabase(databaseName: "BankDb")
           .Options;

        DataContext context;
        UserRepo userRepo;

        [OneTimeSetUp]
        public void Setup()
        {
            context = new DataContext(dbContextOptions);
            context.Database.EnsureCreated();

            SeedDatabase();

            userRepo = new UserRepo(context,configuration.Object);
        }
        [OneTimeTearDown]
        public void CleanUp()
        {
            context.Database.EnsureDeleted();
        }

        private void SeedDatabase()
        {


            var userDetails = new List<UserDetail>()
            {
                new UserDetail()
                {
                   Id = 1, Username ="Vaish", Email="dfytduj", Balance= 5789
                },
                new UserDetail()
                {
                    Id = 2, Username ="Ram", Email="iyiiuui", Balance=79867
                },
                new UserDetail()
                {
                   Id = 3, Username ="Thanya", Email ="dhdhujyfi", Balance=6585
                },
            };
            context.UserDetails.AddRange(userDetails);
            var users = new List<UserLogin>()
            {
                new UserLogin()
                {
                   id = 1, Username ="Jayarath", Password="123456", Role= "Employee"
                },
                new UserLogin()
                {
                    id = 2, Username ="Vaish", Password="12345", Role="User"
                },
                new UserLogin()
                {
                   id = 3, Username ="Ram", Password ="12345", Role="User"
                },
                new UserLogin()
                {
                   id = 4, Username ="Thanya", Password ="12345", Role="User"
                },
            };
            context.Users.AddRange(users);

            context.SaveChanges();
        }
        [Test]
        public async Task UserLogin_Success_test()
        {
            Mock<IConfiguration> configuration = new Mock<IConfiguration>();
            configuration.Setup(c => c.GetSection("Jwt:Key").Value).Returns("BankManagementSecurityCode");
            UserRepo userRepo = new UserRepo(context, configuration.Object);
            ResponseObject result = await userRepo.Login(new UserLoginDto { Username="Vaish", Password="12345" });

            Assert.IsTrue(result.Status);
        }
        [Test]
        public async Task UserLogin_WithUserCredential_test()
        {
            Mock<IConfiguration> configuration = new Mock<IConfiguration>();
            configuration.Setup(c => c.GetSection("Jwt:Key").Value).Returns("BankManagementSecurityCode");
            UserRepo userRepo = new UserRepo(context, configuration.Object);
            ResponseObject result = await userRepo.Login(new UserLoginDto { Username="Jayarath", Password="123456" });

            Assert.That("Access denied", Is.EqualTo(result.Message));
        }
        [Test]
        public async Task UserLogin_IncorrectCredential_test()
        {
            Mock<IConfiguration> configuration = new Mock<IConfiguration>();
            configuration.Setup(c => c.GetSection("Jwt:Key").Value).Returns("BankManagementSecurityCode");
            UserRepo userRepo = new UserRepo(context, configuration.Object);
            ResponseObject result = await userRepo.Login(new UserLoginDto { Username="Arun", Password="123456" });

            Assert.IsFalse(result.Status);
        }
        [Test]
        public async Task WithdrawAmount_WithCorrectAmount_Test()
        {
            UserRepo userRepo = new UserRepo(context, configuration.Object);
            ResponseObject result = await userRepo.WithdrawAmount("Thanya", 87);

            Assert.IsTrue(result.Status);
        }
        [Test]
        public async Task WithdrawAmount_WithAmountGreaterThanBalance_Test()
        {
            UserRepo userRepo = new UserRepo(context, configuration.Object);
            ResponseObject result = await userRepo.WithdrawAmount("Thanya",869699);

            Assert.That("Amount exceeds the balance",Is.EqualTo(result.Message));
            Assert.IsFalse(result.Status);
        }
        [Test]
        public async Task WithdrawAmount_WithIncorrectUsername_Test()
        {
            UserRepo userRepo = new UserRepo(context, configuration.Object);
            ResponseObject result = await userRepo.WithdrawAmount("Arun", 657);

            Assert.That("User name not found", Is.EqualTo(result.Message));
            Assert.IsFalse(result.Status);
        }
        [Test]
        public async Task DepositAmount_WithCorrectUsername_Test()
        {
            UserRepo userRepo = new UserRepo(context, configuration.Object);
            bool result = await userRepo.DepositAmount("Thanya", 657);

            Assert.IsTrue(result);
        }
        [Test]
        public async Task DepositAmount_WithInCorrectUsername_Test()
        {
            UserRepo userRepo = new UserRepo(context, configuration.Object);
            bool result = await userRepo.DepositAmount("Arun", 657);

            Assert.IsFalse(result);
        }
    }
}
