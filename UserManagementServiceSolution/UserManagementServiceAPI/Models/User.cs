using System.ComponentModel.DataAnnotations;

namespace UserManagementServiceAPI.Models
{
    public class User
    {
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int UserAge { get; set; }
        public string Role { get; set; } = string.Empty;
        public byte[] Password { get; set; }
        public byte[] Key { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}
