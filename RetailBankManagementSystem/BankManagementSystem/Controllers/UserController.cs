using BankManagementSystem.Models;
using BankManagementSystem.Models.DTO;
using BankManagementSystem.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="User")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepo _authRepo;

        public UserController(IUserRepo authRepo)
        {
            _authRepo = authRepo;

        }

        //[HttpGet, Authorize]
        //public ActionResult<string> GetMe()
        //{
        //  //  string result = _authRepo.GetUserFromClaim();
        //    return User.Identity.Name;
        //}

        [HttpPost]
        [Route("Login"),AllowAnonymous]
        public async Task<ActionResult> Authentication(UserLoginDto user)
        {


           // if (user.Username == "" || user.Password == "") return BadRequest("Username or password is not provided");

            var result = await _authRepo.Login(user);

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
        [Route("Withdraw")]
        public async Task<ActionResult> AmountWithdrawControl(string Username, double Amount)
        {
            ResponseObject result = await _authRepo.WithdrawAmount(Username, Amount);
            if (result.Status)
            {
                return Ok(result.Value);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpPost]
        [Route("Deposit")]
        public async Task<ActionResult> AmountDepositControl(string Username, double Amount)
        {
            bool result = await _authRepo.DepositAmount(Username, Amount);
            if (result)
            {
                return Ok("Amount Credited successfully");
            }
            else
            {
                return BadRequest("Username not found");
            }
        }
    }
}
