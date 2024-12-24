using UserManagementServiceAPI.Models;
using UserManagementServiceAPI.Models.DTOs;

namespace UserManagementServiceAPI.Interfaces
{
    public interface IUserRepository
    {
        Task<User> AddUser(User user);
        Task<ICollection<User>> GetAllUsers();
        Task<User> GetUser(string userName);
        Task<User> UpdateUser(User user);
        Task<User> DeleteUser(string userName);
    }
}
