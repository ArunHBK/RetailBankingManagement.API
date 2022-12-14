using BankManagementSystem.Data;
using BankManagementSystem.Models.DTO;
using BankManagementSystem.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using BankManagementSystem.Repository.IRepository;

namespace BankManagementSystem.Repository
{
    public class UserRepo : IUserRepo
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        //private readonly IHttpContextAccessor _httpContextAccessor;

        public UserRepo(DataContext context,IConfiguration configuration/*,IHttpContextAccessor httpContextAccessor*/)
        {
            _context = context;
            _configuration = configuration;
           // _httpContextAccessor = httpContextAccessor;
        }
        //public string GetUserFromClaim()
        //{
        //    string value = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
        //    return value;
        //}
        public async Task<ResponseObject> Login(UserLoginDto user)
        {

            var response = new ResponseObject();
            var userCheck = await _context.Users.Where(m => m.Username == user.Username && m.Password == user.Password).FirstOrDefaultAsync();

            if (userCheck != null)
            {
                if (userCheck.Role != "User")
                {
                    response.Status = false;
                    response.Message = "Access denied";
                    return response;
                }
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Role, userCheck.Role),
                    new Claim(ClaimTypes.Name, user.Username)
                };

                var token = new JwtSecurityToken(
                    issuer: "https://localhost:44338/",
                    audience: "https://localhost:44338/",
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: credentials);

                string returnData = new JwtSecurityTokenHandler().WriteToken(token);
                response.Status = true;
                response.Message = returnData;
                return response;
            }
            else
            {
                response.Status = false;
                response.Message ="Invalid username and password";
                return response;
            }

        }

        public async Task<ResponseObject> WithdrawAmount(string Username, double Amount)
        {
            var result = await _context.UserDetails.Where(b => b.Username == Username).FirstOrDefaultAsync();
            ResponseObject transactionResult = new ResponseObject();
            if (result != null)
            {

                double PreviousBalance = result.Balance;

                if (Amount > result.Balance)
                {
                    transactionResult.Status = false;
                    transactionResult.Message = "Amount exceeds the balance";
                    
                    return transactionResult;
                }
                else
                {
                    Transaction transaction = new Transaction();

                    result.Balance = result.Balance -Amount;
                    transaction.Username = result.Username;
                    transaction.ClosingBalance = result.Balance;
                    transaction.TransactionType = "Withdrawal";
                    transaction.Amount = Amount;
                    transaction.TransactionDate= DateTime.Now;
                    _context.Transactions.Add(transaction);
                    await _context.SaveChangesAsync();
                    transactionResult.Status = true;
                    transactionResult.Message =$"Before Balance :{PreviousBalance} After Balance :{result.Balance}";
                   // transactionResult.Value = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
                    return transactionResult;
                }

            }
            else
            {
                transactionResult.Status = false;
                transactionResult.Message = "User name not found";
                return transactionResult;
            }
        }

        public async Task<bool> DepositAmount(string Username, double Amount)
        {
            var result = await _context.UserDetails.Where(b => b.Username == Username).FirstOrDefaultAsync();
            if (result != null)
            {
                Transaction transaction = new Transaction();

                result.Balance = result.Balance + Amount;

                transaction.Username = result.Username;
                transaction.ClosingBalance = result.Balance;
                transaction.TransactionType = "Deposit";
                transaction.Amount = Amount;
                transaction.TransactionDate= DateTime.Now;

                _context.Transactions.Add(transaction);

                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

       
    }
}
