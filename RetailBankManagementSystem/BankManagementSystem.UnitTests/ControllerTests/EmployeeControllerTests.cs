using BankManagementSystem.Controllers;
using BankManagementSystem.Models;
using BankManagementSystem.Models.DTO;
using BankManagementSystem.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagementSystem.UnitTests.ControllerTests
{

    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class EmployeeControllerTests
    {
        [SetUp]
        public void Setup()
        {
        }
        [Test]
        public async Task Employee_Authentication_ReturnOk_Test()
        {
            UserLoginDto mockInputForAuthentication = new UserLoginDto { Username = "Vaish", Password = "12345" };
            var mockEmployeeRepo = new Mock<IEmployeeRepo>();
            mockEmployeeRepo.Setup(x => x.Login(mockInputForAuthentication))
            .ReturnsAsync(new ResponseObject { Status= true });
          
            EmployeeController employeeController = new EmployeeController(mockEmployeeRepo.Object);
            var result = await employeeController.Authentication(mockInputForAuthentication);
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.NotNull(result);
        }
        [Test]
        public async Task Employee_Authentication_ReturnBadRequest_Test()
        {
            UserLoginDto mockInputForAuthentication = new UserLoginDto { Username = "Vaish", Password = "12345" };
            var mockEmployeeRepo = new Mock<IEmployeeRepo>();
            mockEmployeeRepo.Setup(x => x.Login(mockInputForAuthentication))
           .ReturnsAsync(new ResponseObject { Status= false });

            EmployeeController employeeController = new EmployeeController(mockEmployeeRepo.Object);
            var result = await employeeController.Authentication(mockInputForAuthentication);
            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
            Assert.NotNull(result);
        }

        [Test]
        public async Task Employee_UserRegistration_ReturnOk_Test()
        {
            UserDetailDto mockInputForUserRegistration = new UserDetailDto{ Username = "Vaish", Password = "12345", Email = "uyfufuy", Balance =58688 };
            Mock<IEmployeeRepo> mockEmployeeRepo = new Mock<IEmployeeRepo>();
            mockEmployeeRepo.Setup(x => x.UserRegistration (mockInputForUserRegistration))
           .ReturnsAsync(true);

            EmployeeController employeeController = new EmployeeController(mockEmployeeRepo.Object);
            var result = await employeeController.Registration(mockInputForUserRegistration);
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.NotNull(result);
        }
        [Test]
        public async Task Employee_UserRegistration_ReturnBadRequest_Test()
        {
            UserDetailDto mockInputForUserRegistration = new UserDetailDto { Username = "Vaish", Password = "12345", Email = "uyfufuy", Balance =58688 };
            var mockEmployeeRepo = new Mock<IEmployeeRepo>();
            mockEmployeeRepo.Setup(x => x.UserRegistration(mockInputForUserRegistration))
           .ReturnsAsync(false);

            EmployeeController employeeController = new EmployeeController(mockEmployeeRepo.Object);
            var result = await employeeController.Registration(mockInputForUserRegistration);
            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
            Assert.NotNull(result);
        }
        [Test]
        public async Task Employee__UserDetailsUpdate_ReturnOk_Test()
        {
            UserDetailUpdateDto mockInputForUserDetailsUpdate = new UserDetailUpdateDto {Username = "Vaish", Password = "12345", Email = "uyfufuy" };
            var mockEmployeeRepo = new Mock<IEmployeeRepo>();
            mockEmployeeRepo.Setup(x => x.UserDetailsUpdate(1,mockInputForUserDetailsUpdate))
           .ReturnsAsync(true);

            EmployeeController employeeController = new EmployeeController(mockEmployeeRepo.Object);
            var result = await employeeController.UpdateDetails(1,mockInputForUserDetailsUpdate);
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.NotNull(result);
        }

        [Test]
        public async Task Employee__UserDetailsUpdate_ReturnBadRequest_Test()
        {
            UserDetailUpdateDto mockInputForUserDetailsUpdate = new UserDetailUpdateDto {Username = "Vaish", Password = "12345", Email = "uyfufuy"};
            var mockEmployeeRepo = new Mock<IEmployeeRepo>();
            mockEmployeeRepo.Setup(x => x.UserDetailsUpdate(1,mockInputForUserDetailsUpdate))
           .ReturnsAsync(false);

            EmployeeController employeeController = new EmployeeController(mockEmployeeRepo.Object);
            var result = await employeeController.UpdateDetails(1,mockInputForUserDetailsUpdate);
            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
            Assert.NotNull(result);
        }
        [Test]
        public async Task Employee__UserAccountDelete_ReturnOk_Test()
        {
            var mockEmployeeRepo = new Mock<IEmployeeRepo>();
            mockEmployeeRepo.Setup(x => x.AccountDelete("Vaish"))
           .ReturnsAsync(true);

            EmployeeController employeeController = new EmployeeController(mockEmployeeRepo.Object);
            var result = await employeeController.UserDelete("Vaish");
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.NotNull(result);
        }
        [Test]
        public async Task Employee__UserAccountDelete_ReturnBadRequest_Test()
        {
            var mockEmployeeRepo = new Mock<IEmployeeRepo>();
            mockEmployeeRepo.Setup(x => x.AccountDelete("Vaish"))
           .ReturnsAsync(false);

            EmployeeController employeeController = new EmployeeController(mockEmployeeRepo.Object);
            var result = await employeeController.UserDelete("Vaish");
            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
            Assert.NotNull(result);
        }
    }
}
