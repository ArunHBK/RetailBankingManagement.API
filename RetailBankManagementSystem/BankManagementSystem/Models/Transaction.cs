using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankManagementSystem.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public DateTime TransactionDate { get; set; }
        [Required]
        public string TransactionType { get; set; } = string.Empty;
        [Required]
        public double Amount { get; set; }
        [Required]
        public double ClosingBalance { get; set; }
    }
}
