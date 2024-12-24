using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity.Data;
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

        public UserService(IUserRepository userRepository,IConfiguration configuration)
        {
            _userRepository = userRepository;
            _key = Encoding.UTF8.GetBytes(configuration["Keys:TokenKey"] ?? "");
        }
        private readonly byte[] _key;

        
        public async Task<string> GenerateToken(UserDTO user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role)
            };
            var symmetricSecurityKey = new SymmetricSecurityKey(_key);
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(30),
                SigningCredentials = signingCredentials
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }

        public async Task<UserDTO> GetUserById(string userId)
        {
            var usertofetch = await _userRepository.GetUser(userId);
            if (usertofetch == null)
            {
                throw new Exception("User not found in the database");
            }
            return new UserDTO
            {
                UserId = usertofetch.UserId,
                UserName = usertofetch.UserName,
                Email = usertofetch.Email,
                Role = usertofetch.Role,
                Token = await GenerateToken(new UserDTO
                {
                    UserName = usertofetch.UserName,
                    Role = usertofetch.Role,
                    Email = usertofetch.Email
                })
            };
        }

        public async Task<UserDTO> LoginUser(UserLoginDTO userLoginDTO)
        {
            var user = await _userRepository.GetUser(userLoginDTO.UserName);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            HMACSHA256 hmac = new HMACSHA256(user.Key);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userLoginDTO.Password));
            if (!computedHash.SequenceEqual(user.Password))
            {
                throw new Exception("Invalid password");
            }
            var userResponse = new UserDTO
            {
                UserName = user.UserName,
                Role = user.Role,
                Email = user.Email,
                Token = ""

            };
            userResponse.Token = await GenerateToken(userResponse);
            return userResponse;

        }

        public async Task<UserRegisterDTO> RegisterUser(UserRegisterDTO userRegisterDTO)
        {
            HMACSHA256 hmac = new HMACSHA256();
            if (userRegisterDTO == null) { throw new Exception("Invalid data/No values passed"); }
            if (userRegisterDTO.UserName == null || userRegisterDTO.UserName == "") { throw new Exception("Invalid UserName"); }
            if (userRegisterDTO.Password == null || userRegisterDTO.Password == "") { throw new Exception("Invalid Password"); }
            if (userRegisterDTO.Email == null || userRegisterDTO.Email == "") { throw new Exception("Invalid Email"); }
            if(userRegisterDTO.Role != "Admin" && userRegisterDTO.Role != "Patient" && userRegisterDTO.Role != "Doctor") { throw new Exception("Invalid Role"); }
            var user = new User();

            user.UserName = userRegisterDTO.UserName;
            user.Email = userRegisterDTO.Email;
            user.Role = userRegisterDTO.Role;
            user.Key = hmac.Key;
            user.Password = hmac.ComputeHash(Encoding.UTF8.GetBytes(userRegisterDTO.Password));
            user.UserAge = userRegisterDTO.UserAge;
            user.UserId = Guid.NewGuid().ToString();
            user.Token = await GenerateToken(new UserDTO
            {
                UserName = userRegisterDTO.UserName,
                Role = userRegisterDTO.Role,
                Email = userRegisterDTO.Email,
            });
            await _userRepository.AddUser(user);
            userRegisterDTO.Token = user.Token;
            return userRegisterDTO;


        }


        public async Task<User> UpdateUser(UserDTO userDTO)
        {
            var user = await _userRepository.GetUser(userDTO.UserName);
            if (user == null)
            {
                throw new Exception("User not found in the database");
            }
            
            var updatedUser =await _userRepository.UpdateUser(new User
            {
                UserName = userDTO.UserName,
                Email = userDTO.Email,
                Role = userDTO.Role,
            });
            return updatedUser;
        }
    }
}
