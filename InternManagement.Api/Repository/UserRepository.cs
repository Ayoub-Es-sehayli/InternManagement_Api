using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternManagement.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace InternManagement.Api.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly InternContext _context;
        public UserRepository(InternContext context)
        {
            _context = context;
        }
        public async Task<User> AddUserAsync(User model)
        {
            await _context.Users.AddAsync(model);
            await SaveChangesAsync();
            return await _context.Users.OrderBy(user => user.Id).LastAsync();
        }

        public async Task<User> FirstOrDefaultAsync(int id)
        {
            var user = await _context.Users.Where(i => i.Id == id).FirstOrDefaultAsync();
            return user;
        }

        public async Task<int> GetUserCountAsync()
        {
            return await _context.Users.CountAsync();
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users.Select(user => new User
            {
                Id = user.Id,
                LastName = user.LastName,
                FirstName = user.FirstName,
                Email = user.Email,
            })
            .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<User> UserExistsAsync(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == username && x.Password == password);
            return user;

        }

    }
}
