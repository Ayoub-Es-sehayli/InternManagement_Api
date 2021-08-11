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
      model.Id = await GetInternCountAsync() + 1;
      await _context.AddAsync<Intern>(model);
      await SaveChangesAsync();
      return await _context.Interns.OrderBy(intern => intern.Id).LastAsync();
    }

    public async Task<Intern> GetInternAsync(int id)
    {
      var intern = await _context.Interns
      .Include(i => i.Division)
        .ThenInclude(d => d.Department)
      .Include(i => i.Decision)
      .Include(i => i.Attendance)
      .Where(i => i.Id == id)
      .FirstOrDefaultAsync();
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
      return await _context.Interns
      .Include(i => i.Decision)
      .Select(intern => new Intern
      {
        Id = intern.Id,
        FirstName = intern.FirstName,
        LastName = intern.LastName,
        Division = intern.Division,
        Decision = intern.Decision,
        State = intern.State
      })
      .Where(intern => intern.State != eInternState.FileClosed)
      .ToListAsync();
    }

    public async Task<bool> SetDecisionForIntern(int id, Decision decision)
    {
      if (await this.InternExistsAsync(id))
      {
        var intern = await this.GetInternAsync(id);
        if (intern.Decision == null)
        {
          await _context.AddAsync<Decision>(decision);
        }
        else
        {
          intern.Decision.Code = decision.Code;
          intern.Decision.Date = decision.Date;
        };
        switch (intern.State)
        {
          case eInternState.ApplicationFilled:
          case eInternState.AssignedDecision:
            intern.State = eInternState.AssignedDecision;
            break;
          default:
            break;
        }
        await SaveChangesAsync();
        return true;
      }
      return false;
    }
  }
}