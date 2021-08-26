using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternManagement.Api.Dtos;
using InternManagement.Api.Enums;
using InternManagement.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace InternManagement.Api.Repository
{
  public class PunchInRepository : IPunchInRepository
  {
    private readonly InternContext _context;

    public PunchInRepository(InternContext context)
    {
      this._context = context;
    }

    public async Task FlagInternEnterAsync(Attendance model)
    {
      var data = _context.Attendance.Where(attendance =>
      attendance.InternId == model.InternId
      && attendance.date == model.date
      && attendance.Type == eAttendanceType.Absent)
      .FirstOrDefault();

      if (data != null)
      {
        data.time = model.time;
        data.Type = eAttendanceType.Enter;
        await _context.SaveChangesAsync();
      }
    }

    public async Task FlagInternExitAsync(Attendance model)
    {
      var data = await _context.Attendance.Where(attendance =>
        attendance.InternId == model.InternId
        && attendance.date == model.date)
        .FirstOrDefaultAsync();
      if (data != null)
      {
        var exitEntry = new Attendance
        {
          Id = _context.Attendance.Select(a => a.Id).OrderByDescending(a => a).FirstOrDefault() + 1,
          date = model.date,
          time = model.time,
          InternId = model.InternId,
          Type = eAttendanceType.Exit
        };
        await _context.Attendance.AddAsync(exitEntry);
        await _context.SaveChangesAsync();
      }
      else
      {
        throw new Microsoft.EntityFrameworkCore.DbUpdateException("Could not find Entry");
      }
    }

    public async Task<IEnumerable<Intern>> GetAttendanceList()
    {
      var today = DateTime.Today;
      var models = await _context.Interns
      .Include(intern => intern.Attendance
        .Where(x => x.date.Date == today.Date)
        .OrderByDescending(x => x.Type)
        .Take(1))
      .Select(intern => new Intern
      {
        Id = intern.Id,
        FirstName = intern.FirstName,
        LastName = intern.LastName,
        Attendance = intern.Attendance,
        State = intern.State
      })
      .Where(intern =>
      intern.State == eInternState.Started
      && intern.Attendance.All(x => x.date == today))
      .ToListAsync();

      return models;
    }
  }
}