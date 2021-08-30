using System;
using System.Linq;
using System.Threading.Tasks;
using InternManagement.Api.Enums;
using InternManagement.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InternManagement.Api.Repository
{
  public class ScheduledJobsRepository : IScheduledJobsRepository
  {
    private readonly InternContext _context;

    public ScheduledJobsRepository(InternContext context)
    {
      this._context = context;
    }

    public async Task<int> AddInternAttendanceEntryAsync()
    {
      var today = DateTime.Today.Date;
      var count = 0;

      var interns = _context.Interns
        .Include(x => x.Attendance)
        .Where(x => x.State == eInternState.Started);

      count += await interns.CountAsync();
      await interns.ForEachAsync(x =>
      {
        x.Attendance.Add(new Attendance
        {
          date = today,
          time = today,
          Type = eAttendanceType.Absent
        });
      });
      await _context.SaveChangesAsync();
      return count;
    }

    public async Task<int> FlagExcessiveAbsenceAsync()
    {
      var count = 0;
      var monday = DateTime.Today.AddDays(-((int)DateTime.Today.DayOfWeek - (int)DayOfWeek.Monday));
      var maxAbsence = (await _context.Preferences.FirstOrDefaultAsync()).nAttendanceDays;

      var interns = _context.Interns
        .Include(x => x.Attendance)
        .Where(x => x.State == eInternState.Started || x.State == eInternState.Finished)
        .Where(x => x.AttendanceAlarmState != eAttendanceAlarmState.ExcessiveAbsence)
        .Where(
          x => x.Attendance
            .Count(y => y.date >= monday && y.Type == eAttendanceType.Absent)
            >= maxAbsence
          );
      count = await interns.CountAsync();
      await interns.ForEachAsync(x =>
      {
        x.AttendanceAlarmState = eAttendanceAlarmState.ExcessiveAbsence;
      });

      return count;
    }

    public async Task<int> UpdateInternStateAsync()
    {
      var today = DateTime.Today.Date;
      var count = 0;
      var interns = _context.Interns
        .Where(x => x.State != eInternState.Cancelled && x.State != eInternState.FileClosed);

      // Starting
      var starting = interns
        .Where(x => x.StartDate.Date == today && x.State == eInternState.AssignedDecision);
      count += await starting.CountAsync();
      await starting.ForEachAsync(x =>
      {
        x.State = eInternState.Started;
      });

      // Ended
      var ended = interns.Where(x => x.EndDate == today && x.State == eInternState.Started).AsQueryable();
      count += await ended.CountAsync();
      await ended.ForEachAsync(x => x.State = eInternState.Finished);

      await _context.SaveChangesAsync();
      return count;
    }
  }
}