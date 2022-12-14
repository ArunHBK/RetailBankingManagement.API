using System.ComponentModel.DataAnnotations;

namespace BankManagementSystem.Models.DTO
{
    public class UserDetailDto
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public double Balance { get; set; }
    }
}
