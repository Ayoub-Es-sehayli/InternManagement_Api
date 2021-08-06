using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternManagement.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace InternManagement.Api.Repository
{
  public class UiRepository : IUiRepository
  {
    private readonly InternContext _context;

    public UiRepository(InternContext context)
    {
      this._context = context;
    }
    public async Task<IEnumerable<Department>> GetDepartmentsAsync()
    {
      var models = await _context.Departments
        .Include(d => d.Divisions)
        .ToListAsync();

      return models;
    }
  }
}