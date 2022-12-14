using BankManagementSystem.Models.DTO;
using BankManagementSystem.Models;

namespace BankManagementSystem.Repository.IRepository
{
    public interface IUserRepo
    {
        public Task<ResponseObject> Login(UserLoginDto user);
        public Task<ResponseObject> WithdrawAmount(string Username, double Amount);
        public Task<bool> DepositAmount(string Username, double Amount);
    }
}
