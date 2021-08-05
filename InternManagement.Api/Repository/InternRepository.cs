using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternManagement.Api.Enums;
using InternManagement.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace InternManagement.Api.Repository
{
  public class InternRepository : IInternRepository
  {
    private readonly InternContext _context;

    public InternRepository(InternContext context)
    {
      _context = context;
    }
    public async Task<Intern> AddInternAsync(Intern model)
    {
      await _context.Interns.AddAsync(model);
      await SaveChangesAsync();
      return await _context.Interns.OrderBy(intern => intern.Id).LastAsync();
    }

    public async Task<Intern> GetInternAsync(int id)
    {
      var intern = await _context.Interns.Where(i => i.Id == id).FirstOrDefaultAsync();
      return intern;
    }

    public async Task<int> GetInternCountAsync()
    {
      return await _context.Interns.CountAsync();
    }

    public async Task<bool> InternExistsAsync(int id)
    {
      var count = await _context.Interns.CountAsync(intern => intern.Id == id);

      return count != 0;
    }

    private async Task SaveChangesAsync()
    {
      await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Intern>> GetInternsAsync()
    {
      return await _context.Interns.Select(intern => new Intern
      {
        Id = intern.Id,
        FirstName = intern.FirstName,
        LastName = intern.LastName,
        Division = intern.Division,
        State = intern.State
      })
      .Where(intern => intern.State != eInternState.FileClosed)
      .ToListAsync();
    }
  }
}