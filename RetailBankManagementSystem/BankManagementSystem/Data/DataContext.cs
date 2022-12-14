
using Microsoft.EntityFrameworkCore;
using BankManagementSystem.Models;

namespace BankManagementSystem.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<UserLogin> Users { get; set; }
        public DbSet<UserDetail> UserDetails { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
