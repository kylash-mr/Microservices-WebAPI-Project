using System.ComponentModel.DataAnnotations;

namespace UserManagementServiceAPI.Models.DTOs
{
    public class UserRegisterDTO
    {
        [Required(ErrorMessage ="Username is mandatory")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is mandatory")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is mandatory")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Role is mandatory")]
        public string Role { get; set; } = string.Empty;

        public string Token { get; set; } = string.Empty;

        public int UserAge { get; set; }
    }
}
