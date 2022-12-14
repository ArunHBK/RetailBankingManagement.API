using BankManagementSystem.Data;
using BankManagementSystem.Models;
using BankManagementSystem.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BankManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]
    [AllowAnonymous]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepo _authRepo;
        private readonly ILogger<TransactionController> _logger;

      


        public TransactionController(ITransactionRepo authRepo, ILogger<TransactionController> logger)
        {
            _authRepo = authRepo;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetAllTransactions")]
        public  async Task<ActionResult> GetTransactionDetailsByUsername(string Username)
        {
            _logger.LogInformation("Vanakkam di maplaii");
            ResponseObject result = await _authRepo.ViewByUsername(Username);
            if (result.Status)
            {
                return Ok(result.Value);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
    }
}
