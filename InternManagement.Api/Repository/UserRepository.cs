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
        public async Task<User> DeleteUserAsync(int id)
        {
            var user = await GetUserByIdAsync(id);
            _context.Users.Remove(user);
            await SaveChangesAsync();
            return user;
        }
        public async Task<User> EditUserAsync(int id, User model)
        {
            var user = await GetUserByIdAsync(id);
            if (user != null)
            {
                user.Email = model.Email;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Role = model.Role;
                _context.Users.Update(user);
                await SaveChangesAsync();
            }
            return user;
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
        public async Task<User> GetUserByIdAsync(int id)
        {
            var user = await _context.Users.Select(u => new User
            {
                Id = u.Id,
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Role = u.Role
            }).Where(i => i.Id == id).FirstOrDefaultAsync();
            return user;
        }
    }
}
