using UserManagementServiceAPI.Interfaces;
using UserManagementServiceAPI.Models;
using UserManagementServiceAPI.Models.DTOs;
using UserManagementServiceAPI.Repositories;

namespace UserManagementServiceAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<UserDTO> GetUserById(int userId)
        {
            var usertofetch = await _userRepository.GetUserById(userId);
            if (usertofetch == null)
            {
                throw new Exception("User not found in the database");
            }
            return usertofetch;
        }

        public Task<UserLoginDTO> LoginUser(UserLoginDTO userLoginDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<UserRegisterDTO> RegisterUser(UserRegisterDTO userRegisterDTO)
        {
            if (userRegisterDTO == null) { throw new Exception("Invalid data/No values passed"); }
            if (userRegisterDTO.UserName == null || userRegisterDTO.UserName == "") { throw new Exception("Invalid UserName"); }
            if (userRegisterDTO.Password == null || userRegisterDTO.Password == "") { throw new Exception("Invalid Password"); }
            if (userRegisterDTO.Email == null || userRegisterDTO.Email == "") { throw new Exception("Invalid Email"); }
            if(userRegisterDTO.Role == null || userRegisterDTO.Role != "Patient" || userRegisterDTO.Role != "Doctor") { throw new Exception("Invalid Role"); }
            var user = new User
            {
                UserName = userRegisterDTO.UserName,
                Password = userRegisterDTO.Password,
                Email = userRegisterDTO.Email,
                PhoneNumber = userRegisterDTO.PhoneNumber
            };
            await _userRepository.AddUser(user);
            return userRegisterDTO;


        }
    }
}
