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

        private readonly byte[] _key;
        public UserService(IUserRepository userRepository,IConfiguration configuration)
        {
            _userRepository = userRepository;
            _key = Encoding.UTF8.GetBytes(configuration["Keys:TokenKey"] ?? throw new ArgumentNullException("TokenKey is not configured."));
        }

        
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
            var existingUser = await _userRepository.GetUserById(userLoginDTO.UserName);
            if (existingUser == null)
            {
                throw new Exception("User not found");
            }
            HMACSHA256 hmac = new HMACSHA256(existingUser.Key);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userLoginDTO.Password));
            if (!computedHash.SequenceEqual(existingUser.Password))
            {
                throw new Exception("Invalid password");
            }
            return new UserDTO
            {
                UserId = existingUser.UserId, 
                UserName = existingUser.UserName,
                Email = existingUser.Email,
                Role = existingUser.Role,
                Token = await GenerateToken(new UserDTO 
                {
                    UserName = existingUser.UserName,
                    Role = existingUser.Role,
                    Email = existingUser.Email
                })
            };

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


    }
}
