using BankManagementSystem.Controllers;
using BankManagementSystem.Models;
using BankManagementSystem.Models.DTO;
using BankManagementSystem.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace BankManagementSystem.UnitTests.ControllerTests
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class UserControllerTests
    {
        [SetUp]
        public void Setup()
        {
        }
        [Test]
        public async Task User_Authentication_ReturnOk_Test()
        {
            UserLoginDto mockInputForAuthentication = new UserLoginDto { Username = "Vaish", Password = "12345" };
            var mockUserRepo = new Mock<IUserRepo>();
            mockUserRepo.Setup(x => x.Login(mockInputForAuthentication))
            .ReturnsAsync(new ResponseObject { Status= true });

            UserController userController = new UserController(mockUserRepo.Object);
            var result = await userController.Authentication(mockInputForAuthentication);
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.NotNull(result);
        }
        [Test]
        public async Task User_Authentication_ReturnBadRequest_Test()
        {
            UserLoginDto mockInputForAuthentication = new UserLoginDto { Username = "Vaish", Password = "12345" };
            var mockUserRepo = new Mock<IUserRepo>();
            mockUserRepo.Setup(x => x.Login(mockInputForAuthentication))
            .ReturnsAsync(new ResponseObject { Status= false });

            UserController userController = new UserController(mockUserRepo.Object);
            var result = await userController.Authentication(mockInputForAuthentication);
            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
            Assert.NotNull(result);
        }
        [Test]
        public async Task User_AmountWithdraw_ReturnOk_Test()
        {
            var mockUserRepo = new Mock<IUserRepo>();
            mockUserRepo.Setup(x => x.WithdrawAmount("Vaish", 2335))
            .ReturnsAsync(new ResponseObject { Status= true });

            UserController userController = new UserController(mockUserRepo.Object);
            var result = await userController.AmountWithdrawControl("Vaish", 2335);
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.NotNull(result);
        }
        [Test]
        public async Task User_AmountWithdraw_ReturnBadRequest_Test()
        {
            var mockUserRepo = new Mock<IUserRepo>();
            mockUserRepo.Setup(x => x.WithdrawAmount("Vaish", 2335))
            .ReturnsAsync(new ResponseObject { Status= false });

            UserController userController = new UserController(mockUserRepo.Object);
            var result = await userController.AmountWithdrawControl("Vaish", 2335);
            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
            Assert.NotNull(result);
        }
        [Test]
        public async Task User_AmountDeposit_ReturnOk_Test()
        {
            var mockUserRepo = new Mock<IUserRepo>();
            mockUserRepo.Setup(x => x.DepositAmount("Vaish", 2335))
            .ReturnsAsync(true);

            UserController userController = new UserController(mockUserRepo.Object);
            var result = await userController.AmountDepositControl("Vaish", 2335);
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.NotNull(result);
        }
        [Test]
        public async Task User_AmountDeposit_ReturnBadRequest_Test()
        {
            var mockUserRepo = new Mock<IUserRepo>();
            mockUserRepo.Setup(x => x.DepositAmount("Vaish", 2335))
            .ReturnsAsync(false);

            UserController userController = new UserController(mockUserRepo.Object);
            var result = await userController.AmountDepositControl("Vaish", 2335);
            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
            Assert.NotNull(result);
        }
    }
}
