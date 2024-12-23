using System.ComponentModel.DataAnnotations;

namespace UserManagementServiceAPI.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string UserCity { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
