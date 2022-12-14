using System.ComponentModel.DataAnnotations;

namespace BankManagementSystem.Models
{
    public class UserLogin
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string Role { get; set; } = string.Empty;
    }
}
