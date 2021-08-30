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

    public DashboardRepository(InternContext context)
    {
      this._context = context;
    }

    public async Task<IEnumerable<Intern>> GetLatestInternsAsync()
    {
      var query = _context.Interns.Select(intern => new Intern
      {
        Id = intern.Id,
        FirstName = intern.FirstName,
        LastName = intern.LastName,
        State = intern.State,
        StartDate = intern.StartDate
      }).Where(intern =>
        intern.State != eInternState.Cancelled ||
        intern.State != eInternState.FileClosed);

      query = query.OrderByDescending(intern => intern.Id);

      return await query.Take(5).ToListAsync();
    }
    public async Task<IEnumerable<Intern>> GetAlertInternsAsync()
    {
      return (await this.GetInternsWithExceciveAbsence()).Concat(await this.GetInternsWithIncompleteFiles()).Take(5);
    }

    public async Task<IEnumerable<Intern>> GetFinishingInternsAsync()
    {
      var limitDate = DateTime.Now.AddDays(7);

      var query = _context.Interns.Select(intern => new Intern
      {
        Id = intern.Id,
        FirstName = intern.FirstName,
        LastName = intern.LastName,
        EndDate = intern.EndDate,
        State = intern.State
      }).Where(intern =>
      intern.State == eInternState.Started
      && intern.EndDate >= DateTime.Now
      && intern.EndDate <= limitDate)
      .OrderBy(intern => intern.EndDate);

      return await query.Take(5).ToListAsync();
    }

    public async Task<IEnumerable<Intern>> GetInternsWithIncompleteFiles()
    {
      var query = _context.Interns.Where(
        intern => intern.State != eInternState.Cancelled ||
        intern.State != eInternState.FileClosed);

      query = query.Select(intern => new Intern
      {
        Id = intern.Id,
        FirstName = intern.FirstName,
        LastName = intern.LastName,
        FileAlarmState = intern.FileAlarmState,
        State = intern.State
      }).Where(intern =>
      intern.State != eInternState.FileClosed
      && intern.State != eInternState.Finished
      && intern.State != eInternState.Cancelled
      && intern.FileAlarmState == eFileAlarmState.IncompleteFile);

      return await query.Take(5).ToListAsync();
    }

    public async Task<IEnumerable<Intern>> GetInternsWithExceciveAbsence()
    {
      var query = _context.Interns.Select(intern => new Intern
      {
        Id = intern.Id,
        FirstName = intern.FirstName,
        LastName = intern.LastName,
        AttendanceAlarmState = intern.AttendanceAlarmState,
        State = intern.State
      }).Where(intern =>
      intern.State == eInternState.Started
      && intern.AttendanceAlarmState == eAttendanceAlarmState.ExcessiveAbsence);

      return await query.Take(5).ToListAsync();
    }

    public async Task<int> GetAbsenteeCountAsync()
    {
      var yesterday = DateTime.Today.AddDays(-1);
      var query = _context.Attendance
        .Where(attendance => attendance.date == yesterday
          && attendance.Type == eAttendanceType.Absent);

      return await query.CountAsync();
    }

    public async Task<int> GetReadyToFinishCountAsync()
    {
      var limitDate = DateTime.Now.AddDays(7);

      var query = _context.Interns.Select(intern => new Intern
      {
        Id = intern.Id,
        FirstName = intern.FirstName,
        LastName = intern.LastName,
        EndDate = intern.EndDate,
        State = intern.State
      }).Where(intern =>
      intern.State == eInternState.Started
      && intern.EndDate >= DateTime.Now
      && intern.EndDate <= limitDate)
      .OrderBy(intern => intern.EndDate);

      return await query.CountAsync();
    }

    public async Task<int> GetActiveInternsCountAsync()
    {
      var query = _context.Interns.Select(intern => new Intern
      {
        Id = intern.Id,
        State = intern.State
      }).Where(intern =>
      intern.State != eInternState.Finished
      && intern.State != eInternState.FileClosed
      && intern.State != eInternState.Cancelled);

      return await query.CountAsync();
    }
  }
}