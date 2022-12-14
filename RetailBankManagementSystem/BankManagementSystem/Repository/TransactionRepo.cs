using BankManagementSystem.Data;
using BankManagementSystem.Models;
using BankManagementSystem.Repository.IRepository;
using Microsoft.EntityFrameworkCore;


namespace BankManagementSystem.Repository
{
    public class TransactionRepo : ITransactionRepo
    {
        private readonly DataContext _context;
        public TransactionRepo(DataContext context)
        {
            _context=context;
        }
       
        public async Task<ResponseObject> ViewByUsername(string Username)
        {
            var userCheck = await _context.Transactions.Where(m => m.Username == Username).FirstOrDefaultAsync();
            var response = new ResponseObject();
            if (userCheck != null)
            {
                response.Value = userCheck;
                response.Status = true;
                response.Message = "Transaction details";
                return response;
            }
            else
            {
               
                response.Status = false;
                response.Message = "User not Found";
                return response;
            }
        }   
    }
}
