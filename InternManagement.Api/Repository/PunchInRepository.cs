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
      var data = _context.Attendance
      .Select(attendance => new Attendance
      {
        Id = attendance.Id,
        InternId = attendance.InternId,
        date = attendance.date
      }).Where(attendance =>
      attendance.InternId == model.InternId
      && attendance.date == model.date
      && attendance.Type == eAttendanceType.Absent)
      .FirstOrDefault();

      if (data != null)
      {
        _context.ChangeTracker.Clear();
        data.time = model.time;
        data.Type = eAttendanceType.Enter;
        _context.Update(data);
        await _context.SaveChangesAsync();
      }
    }

    public async Task FlagInternExitAsync(Attendance model)
    {
      var data = await _context.Attendance.Select(attendance => new Attendance
      {
        InternId = attendance.InternId,
        date = attendance.date
      }).Where(attendance =>
        attendance.InternId == model.InternId
        && attendance.date == model.date)
        .ToListAsync();
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
      .Include(intern => intern.Attendance)
      .Select(intern => new Intern
      {
        Id = intern.Id,
        FirstName = intern.FirstName,
        LastName = intern.LastName,
        Attendance = intern.Attendance.Where(x => x.date == today).ToList(),
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