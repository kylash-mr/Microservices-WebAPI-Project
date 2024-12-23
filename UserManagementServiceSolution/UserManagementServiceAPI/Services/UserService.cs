using Microsoft.IdentityModel.Tokens;
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
            return new UserDTO
            {
                UserId = usertofetch.UserId,
                UserName = usertofetch.UserName,
                Email = usertofetch.Email,
                PhoneNumber = usertofetch.PhoneNumber,
                Role = usertofetch.Role,
                UserCity = usertofetch.UserCity
            };
        }

        public Task<UserLoginDTO> LoginUser(UserLoginDTO userLoginDTO)
        {
           var username = userLoginDTO.UserName;
           var password = userLoginDTO.Password;
           var usercredInDb = _userRepository.GetUserByUserName(username).Result.Password;
            if(username == null || username == "")
            {
                throw new Exception("Invalid UserName");
            }
            if (password == null || password == "")
            {
                throw new Exception("Invalid Password");
            }
            if (usercredInDb != password)
            {
                throw new Exception("Incorrect Password");
            }
            var user =   new UserLoginDTO
            {
                UserName = username,
                Password = password
            };
            return Task.FromResult(user);


        }

        public async Task<UserRegisterDTO> RegisterUser(UserRegisterDTO userRegisterDTO)
        {
            if (userRegisterDTO == null) { throw new Exception("Invalid data/No values passed"); }
            if (userRegisterDTO.UserName == null || userRegisterDTO.UserName == "") { throw new Exception("Invalid UserName"); }
            if (userRegisterDTO.Password == null || userRegisterDTO.Password == "") { throw new Exception("Invalid Password"); }
            if (userRegisterDTO.Email == null || userRegisterDTO.Email == "") { throw new Exception("Invalid Email"); }
            if(userRegisterDTO.Role != "Patient" && userRegisterDTO.Role != "Doctor") { throw new Exception("Invalid Role. Choose either 'Patient'/'Doctor'"); }
            var user = new User
            {
                UserName = userRegisterDTO.UserName,
                Password = userRegisterDTO.Password,
                Email = userRegisterDTO.Email,
                PhoneNumber = userRegisterDTO.PhoneNumber,
                Role = userRegisterDTO.Role,
                UserCity = userRegisterDTO.UserCity
            };
            await _userRepository.AddUser(user);
            return userRegisterDTO;


        }

        public async Task<User> UpdateUser(int userId, UserDTO userDTO)
        {
            var user = await _userRepository.GetUserById(userId);
            if (user == null)
            {
                throw new Exception("User not found in the database");
            }
            if(userId <= 0 )
            {
                throw new Exception("Invalid UserId");
            }
            var updatedUser =await _userRepository.UpdateUser(userId, new User
            {
                UserId = userDTO.UserId,
                UserName = userDTO.UserName,
                Email = userDTO.Email,
                PhoneNumber = userDTO.PhoneNumber,
                Role = userDTO.Role,
                UserCity = userDTO.UserCity,
                Password = userDTO.Password
            });
            return updatedUser;
        }
    }
}
