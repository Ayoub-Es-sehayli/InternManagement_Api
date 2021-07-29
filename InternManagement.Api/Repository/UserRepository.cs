using System.Linq;
using System.Threading.Tasks;
using InternManagement.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace InternManagement.Api.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly InternContext _context;
        public UserRepository(DbContext context)
        {
            _context = (InternContext)context;
        }
        public async Task<User> AddUserAsync(User model)
        {
            await _context.Users.AddAsync(model);
            await SaveChangesAsync();
            return await _context.Users.OrderBy(user => user.Id).LastAsync();
        }

        public async Task<int> GetUserCountAsync()
        {
            return await _context.Users.CountAsync();
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}