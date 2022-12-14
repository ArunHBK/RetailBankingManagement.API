using System.ComponentModel.DataAnnotations;

namespace BankManagementSystem.Models
{
    public class UserDetail
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
       
        public string Email { get; set; } = string.Empty;
        [Required]
        public double Balance  { get; set; }
    }
}