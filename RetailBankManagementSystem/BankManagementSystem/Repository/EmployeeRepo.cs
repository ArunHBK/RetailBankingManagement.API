using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BankManagementSystem.Data;
using BankManagementSystem.Models;
using BankManagementSystem.Models.DTO;
using BankManagementSystem.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BankManagementSystem.Repository
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
       
        public EmployeeRepo(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration=configuration;
        }
        //public async Task<List<UserDetail>> ViewUserDetails()
        //{
        //    var value = await _context.UserDetails.ToListAsync();
        //    return value;
        //}
        //public async Task<List<UserLogin>> ViewLoginDetails()
        //{
        //    var value = await _context.Users.ToListAsync();
        //    return value;
        //}
        public async Task<ResponseObject> Login(UserLoginDto user)
        {

            var response = new ResponseObject();
            var userCheck = await _context.Users.Where(m => m.Username == user.Username && m.Password == user.Password).FirstOrDefaultAsync();

            if (userCheck != null )
            {
                if (userCheck.Role != "Employee")
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

        public async Task<bool> UserRegistration(UserDetailDto user)
        {
            var userCheck = await _context.UserDetails.Where(m => m.Username == user.Username).FirstOrDefaultAsync();

            

            if (userCheck == null)
            {
                UserLogin LoginUser = new UserLogin();
                LoginUser.Username = user.Username;
                LoginUser.Password = user.Password;
                LoginUser.Role ="User";
                UserDetail mapping = new UserDetail
                {
                    Username = user.Username,
                    Balance = user.Balance, 
                    Email = user.Email

                };
                _context.UserDetails.Add(mapping);
                _context.Users.Add(LoginUser);
                await _context.SaveChangesAsync();
                return true;

            }
            else
            {
                return false;
            }
        }

        public async Task<bool> UserDetailsUpdate(int Id,UserDetailUpdateDto user)
        {
            var result = await _context.UserDetails.FindAsync(Id);
            if (result != null)
            {
                var result2 = await _context.Users.Where(b => b.Username == result.Username).FirstOrDefaultAsync();

                

                result.Username = user.Username;
                result.Email = user.Email;

                if (result2 != null)
                {
                    result2.Username = user.Username;
                    result2.Password = user.Password;
                }

                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> AccountDelete(string Username)
        {

            var value = await _context.UserDetails.Where(b => b.Username == Username).FirstOrDefaultAsync();

            if (value != null)
            {
                _context.UserDetails.Remove(value);
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
