using BankManagementSystem.Models;
using BankManagementSystem.Models.DTO;
using BankManagementSystem.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepo _authRepo;
       

        public EmployeeController(IEmployeeRepo authRepo)
        {
            _authRepo = authRepo;
        }


        //[HttpGet]
        //[Route("GetAllLoginDetails")]
        //public async Task<ActionResult<List<UserLogin>>> GetAllLoginDetails()
        //{
        //    List<UserLogin> UserList = await _authRepo.ViewLoginDetails();
        //    return Ok(UserList);
        //}

        [HttpPost]
        [Route("Login"),AllowAnonymous]
        public async Task<ActionResult> Authentication(UserLoginDto user)
        {


           // if (user.Username == "" || user.Password == "") return BadRequest("Username or password is not provided");

            ResponseObject result = await _authRepo.Login(user);

            if (result.Status)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPost]
        [Route("UserRegister")]
        public async Task<ActionResult> Registration(UserDetailDto user)
        {
           // if (user.Username == "" || user.Password == "") return BadRequest("Username or password is not provided");

            bool result = await _authRepo.UserRegistration(user);

            if (result)
            {
                return Ok("User added successfully");
            }
            else
            {
                return BadRequest("User details already exists");
            }
        }

        [HttpPut]
        [Route("UserDetailsUpdate")]
        public async Task<ActionResult> UpdateDetails(int Id,UserDetailUpdateDto request)
        {
            bool result = await _authRepo.UserDetailsUpdate(Id,request);
            if (result)
            {

                return Ok("User details changed successfully");
            }
            else
            {
                return BadRequest("User not found");
            }
        }

        [HttpDelete("UserAccountDelete")]
        //  [Route("api/{Username}")]
        public async Task<ActionResult> UserDelete(string Username)
        {
            bool result = await _authRepo.AccountDelete(Username);
            if (result)
            {
                return Ok("Account has been deleted successfully");
            }
            else
            {
                return BadRequest("User not found");
            }
        }
    }
}