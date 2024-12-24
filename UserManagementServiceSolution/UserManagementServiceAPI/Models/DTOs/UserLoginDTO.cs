using System.ComponentModel.DataAnnotations;

namespace UserManagementServiceAPI.Models.DTOs
{
    public class UserLoginDTO
    {
        [Required(ErrorMessage = "Username is mandatory")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is mandatory")]
        public string Password { get; set; } = string.Empty;
    }
}
