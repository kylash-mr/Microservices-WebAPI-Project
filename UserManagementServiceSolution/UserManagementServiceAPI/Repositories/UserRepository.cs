using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

        public async Task<ICollection<User>> GetAllUsers()
        {
            if (_userDbContext.Users.Count() == 0)
            {
                throw new Exception("No users found/registered in the database");
            }
            return await _userDbContext.Users.ToListAsync();
        }

        public async Task<User> GetUserById(string userId)
        {
            var user = await _userDbContext.Users.SingleOrDefaultAsync(u => u.UserName == userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            return user;
        }

       
        public async Task<User> UpdateUser(User user)
        {
            var usertoupdate =await  GetUserById(user.UserId.ToString());
            if (usertoupdate == null)
            {
                throw new Exception("User not found in the database");
            }
             _userDbContext.Users.Update(user);
             await _userDbContext.SaveChangesAsync();
            return  usertoupdate;

        }
    }
}
