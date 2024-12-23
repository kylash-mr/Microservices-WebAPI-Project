using System.ComponentModel.DataAnnotations;

namespace UserManagementServiceAPI.Models.DTOs
{
    public class UserLoginDTO
    {
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
