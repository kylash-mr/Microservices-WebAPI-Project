using Microsoft.EntityFrameworkCore;
using UserManagementServiceAPI.DbContexts;
using UserManagementServiceAPI.Interfaces;
using UserManagementServiceAPI.Models;

namespace UserManagementServiceAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _userDbContext;

        public UserRepository(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }

        public async Task<User> AddUser(User user)
        {
            await _userDbContext.Users.AddAsync(user);
            await _userDbContext.SaveChangesAsync();
            return user;
        }

        public Task<User> DeleteUser(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<User>> GetAllUsers()
        {
            if (_userDbContext.Users.Count() == 0)
            {
                throw new Exception("No users found/registered in the database");
            }
            return await _userDbContext.Users.ToListAsync();
        }

        public async Task<User> GetUserById(int userId)
        { 
            if (userId <= 0)
            {
                throw new Exception("Invalid UserId");
            }

            var usertofetch = await _userDbContext.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            if (usertofetch == null)
            {
                throw new Exception("User not found in the database");
            }
            return usertofetch;
        }

        public Task<User> GetUserByUserName(string userName)
        {
            if(userName == null || userName == "")
            {
                throw new Exception("Invalid UserName");
            }
            var user = _userDbContext.Users.FirstOrDefaultAsync(u => u.UserName == userName);
            if (user == null)
            {
                throw new Exception("User not found in the database");
            }
            return user;
        }

        public async Task<User> UpdateUser(int userId, User user)
        {
            var usertoupdate = GetUserById(userId);
            if (usertoupdate == null)
            {
                throw new Exception("User not found in the database");
            }
            if(usertoupdate.Id == user.UserId)
            {
                throw new Exception("User already exists");
            }
            usertoupdate.Result.UserName = user.UserName;
            usertoupdate.Result.Password = user.Password;
            usertoupdate.Result.PhoneNumber = user.PhoneNumber;
            usertoupdate.Result.Email = user.Email;
            usertoupdate.Result.UserCity = user.UserCity;
            usertoupdate.Result.Role = user.Role;
            await _userDbContext.SaveChangesAsync();
            return await usertoupdate;

        }
    }
}
