using BankManagementSystem.Models;

namespace BankManagementSystem.Repository.IRepository
{
    public interface ITransactionRepo
    {
        public Task<ResponseObject> ViewByUsername(string Username);
    }
}
