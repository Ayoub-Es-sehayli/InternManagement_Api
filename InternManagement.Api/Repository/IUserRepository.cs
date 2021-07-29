using System.Threading.Tasks;
using InternManagement.Api.Models;

namespace InternManagement.Api.Repository{
    public interface IUserRepository{
        Task<User> AddUserAsync(User model);
    }
}