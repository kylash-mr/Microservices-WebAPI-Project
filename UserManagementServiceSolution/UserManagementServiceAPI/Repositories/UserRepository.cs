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

        public async Task<User> DeleteUser(string userId)
        {
            var user =  await GetUser(userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }
             _userDbContext.Users.Remove(user);
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

        public async Task<User> GetUser(string userName)
        { 
            if (userName == "" || userName == null)
            {
                throw new Exception("Invalid UserId");
            }

            var usertofetch = await _userDbContext.Users.SingleOrDefaultAsync(u => u.UserName == userName);
            if (usertofetch == null)
            {
                throw new Exception("User not found in the database");
            }
            return usertofetch;
        }

       
        public async Task<User> UpdateUser(User user)
        {
            var usertoupdate =await  GetUser(user.UserName);
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
