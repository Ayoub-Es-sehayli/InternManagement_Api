using System.Collections.Generic;
using System.Threading.Tasks;
using InternManagement.Api.Models;

namespace InternManagement.Api.Repository{
    public interface IUserRepository{
        Task<User> AddUserAsync(User model);
        Task<User> UserExistsAsync(string username, string password);
        Task<User> FirstOrDefaultAsync(int id);
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<User> DeleteUserAsync(int id);
    }
}