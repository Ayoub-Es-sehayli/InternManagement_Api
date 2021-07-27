using System.Linq;
using System.Threading.Tasks;
using InternManagement.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace InternManagement.Api.Repository
{
  public class InternRepository : IInternRepository
  {
    private readonly InternContext _context;

    public InternRepository(DbContext context)
    {
      _context = (InternContext)context;
    }
    public async Task<Intern> AddInternAsync(Intern model)
    {
      await _context.Interns.AddAsync(model);
      await SaveChangesAsync();
      return await _context.Interns.OrderBy(intern => intern.Id).LastAsync();
    }
    public async Task<int> GetInternCountAsync()
    {
      return await _context.Interns.CountAsync();
    }
    private async Task SaveChangesAsync()
    {
      await _context.SaveChangesAsync();
    }
  }
}