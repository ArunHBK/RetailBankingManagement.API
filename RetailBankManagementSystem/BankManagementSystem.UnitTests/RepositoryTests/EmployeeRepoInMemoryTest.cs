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
    public class EmployeeRepoInMemoryTest
    {
        Mock<IConfiguration> configuration = new Mock<IConfiguration>();

        private static DbContextOptions<DataContext> dbContextOptions = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "BankDb")
            .Options;

        DataContext context;
        EmployeeRepo employeeRepo;

        [OneTimeSetUp]
        public void Setup()
        {
            context = new DataContext(dbContextOptions);
            context.Database.EnsureCreated();

            SeedDatabase();

            employeeRepo = new EmployeeRepo(context,configuration.Object);
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
        public async Task EmployeeLogin_Success_test()
        {
            Mock<IConfiguration> configuration = new Mock<IConfiguration>();
            configuration.Setup(c => c.GetSection("Jwt:Key").Value).Returns("BankManagementSecurityCode");
            EmployeeRepo employeeRepo = new EmployeeRepo(context,configuration.Object);
            ResponseObject result = await employeeRepo.Login(new UserLoginDto {Username="Jayarath",Password="123456"});

            Assert.IsTrue(result.Status);
        }
        [Test]
        public async Task EmployeeLogin_WithUserCredential_test()
        {
            Mock<IConfiguration> configuration = new Mock<IConfiguration>();
            configuration.Setup(c => c.GetSection("Jwt:Key").Value).Returns("BankManagementSecurityCode");
            EmployeeRepo employeeRepo = new EmployeeRepo(context, configuration.Object);
            ResponseObject result = await employeeRepo.Login(new UserLoginDto { Username="Vaish", Password="12345" });

            Assert.That("Access denied", Is.EqualTo(result.Message));
        }
        [Test]
        public async Task EmployeeLogin_IncorrectCredential_test()
        {
            Mock<IConfiguration> configuration = new Mock<IConfiguration>();
            configuration.Setup(c => c.GetSection("Jwt:Key").Value).Returns("BankManagementSecurityCode");
            EmployeeRepo employeeRepo = new EmployeeRepo(context, configuration.Object);
            ResponseObject result = await employeeRepo.Login(new UserLoginDto { Username="Arun", Password="123456" });

            Assert.IsFalse(result.Status);
        }
        [Test]
        public async Task UserRegistration_WithNewUsername_Test()
        {
            
            EmployeeRepo employeeRepo = new EmployeeRepo(context, configuration.Object);
            bool result = await employeeRepo.UserRegistration(new UserDetailDto { Username="Arun", Password="123456",Email="dytdtduk",Balance=7800 });

            Assert.IsTrue(result);
        }
        [Test]
        public async Task UserRegistration_WithExistingUsername_Test()
        {

            EmployeeRepo employeeRepo = new EmployeeRepo(context, configuration.Object);
            bool result = await employeeRepo.UserRegistration(new UserDetailDto { Username="Ram", Password="12345", Email="dytdtduk",Balance=5859});

            Assert.IsFalse(result);
        }
        [Test]
        public async Task UserDetailUpdate_WithCorrectUserId_Test()
        {
            EmployeeRepo employeeRepo = new EmployeeRepo(context, configuration.Object);
            bool result = await employeeRepo.UserDetailsUpdate(1,new UserDetailUpdateDto { Username="Jayarath", Password="123456", Email="dytdtduk"});

            Assert.IsTrue(result);
        }
        [Test]
        public async Task UserDetailUpdate_WithInCorrectUserId_Test()
        {
            EmployeeRepo employeeRepo = new EmployeeRepo(context, configuration.Object);
            bool result = await employeeRepo.UserDetailsUpdate(5, new UserDetailUpdateDto { Username="Jayarath", Password="123456", Email="dytdtduk" });

            Assert.IsFalse(result);
        }
        [Test]
        public async Task UserAccountDelete_WithExistingUser_Test()
        {
            EmployeeRepo employeeRepo = new EmployeeRepo(context, configuration.Object);
            bool result = await employeeRepo.AccountDelete("Thanya");

            Assert.IsTrue(result);
        }
        [Test]
        public async Task UserAccountDelete_WithWrongUser_Test()
        {
            EmployeeRepo employeeRepo = new EmployeeRepo(context, configuration.Object);
            bool result = await employeeRepo.AccountDelete("Arun");

            Assert.IsFalse(result);
        }
    }
}
