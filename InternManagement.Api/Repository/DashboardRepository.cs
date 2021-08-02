using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternManagement.Api.Enums;
using InternManagement.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace InternManagement.Api.Repository
{
  public class DashboardRepository : IDashboardRepository
  {
    private readonly InternContext _context;

    public DashboardRepository(DbContext context)
    {
      this._context = (InternContext)context;
    }

    public async Task<IEnumerable<Intern>> GetLatestInternsAsync()
    {
      var query = _context.Interns.Select(intern => new Intern
      {
        Id = intern.Id,
        State = intern.State,
        StartDate = intern.StartDate
      }).Where(intern =>
        intern.State != eInternState.Cancelled ||
        intern.State != eInternState.FileClosed);

      query = query.OrderBy(intern => intern.Id);

      return await query.Take(5).ToListAsync();
    }

  }
}