using UserManagementServiceAPI.Models;
using UserManagementServiceAPI.Models.DTOs;

namespace UserManagementServiceAPI.Interfaces
{
    public interface IUserRepository
    {
        Task<User> AddUser(User user);
        Task<ICollection<User>> GetAllUsers();
        Task<User> GetUserById(int userId);
        Task<User> GetUserByUserName(string userName);
        Task<User> UpdateUser(int userId, User user);
        Task<User> DeleteUser(int userId);
    }
}
