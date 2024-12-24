using UserManagementServiceAPI.Models;
using UserManagementServiceAPI.Models.DTOs;

namespace UserManagementServiceAPI.Interfaces
{
    public interface IUserService
    {
        public Task<UserRegisterDTO> RegisterUser(UserRegisterDTO userRegisterDTO);
        public Task<UserDTO> GetUserById(string userId);
        public Task<UserDTO> LoginUser(UserLoginDTO userLoginDTO);
        public Task<string> GenerateToken(UserDTO user);
    }
}
