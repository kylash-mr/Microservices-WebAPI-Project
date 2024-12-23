using UserManagementServiceAPI.Models;
using UserManagementServiceAPI.Models.DTOs;

namespace UserManagementServiceAPI.Interfaces
{
    public interface IUserService
    {
        public Task<UserRegisterDTO> RegisterUser(UserRegisterDTO userRegisterDTO);
        public Task<UserDTO> GetUserById(int userId);
        public Task<UserLoginDTO> LoginUser(UserLoginDTO userLoginDTO);
    }
}
