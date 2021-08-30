using System;
using System.Linq;
using System.Threading.Tasks;
using InternManagement.Api.Enums;
using InternManagement.Api.Models;
using InternManagement.Api.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace InternManagement.Tests
{
  public class ScheduledJobsRepositoryTests
  {
    private readonly InternContext context;
    private IScheduledJobsRepository repository;
    public ScheduledJobsRepositoryTests()
    {
      context = new MockDbSeed("Jobs_Base").context;
      repository = new ScheduledJobsRepository(context);
    }

    [Fact]
    public async Task UpdateInternStateAsync_ChangesState()
    {
      var minId = 120;
      var maxId = 200;
      var currentDay = DateTime.Today;
      await context.Interns
        .Where(x => x.Id >= minId && x.Id < maxId)
        .ForEachAsync(intern =>
        {
          if (intern.Id % 2 == 0)
          {
            intern.State = eInternState.AssignedDecision;
            intern.StartDate = currentDay;
          }
          else
          {
            intern.State = eInternState.Started;
            intern.EndDate = currentDay;
          }
        });
      await context.SaveChangesAsync();

      var result = await repository.UpdateInternStateAsync();

      Assert.Equal(maxId - minId, result);

      await context.Interns
        .Where(x => x.Id >= minId && x.Id < maxId)
        .ForEachAsync(x =>
        {
          if (x.Id % 2 == 0)
          {
            Assert.Equal(eInternState.Started, x.State);
          }
          else
          {
            Assert.Equal(eInternState.Finished, x.State);
          }
        });
    }

    [Fact]
    public async Task AddInternAttendanceEntryAsync_AddsAttendancePerIntern()
    {
      var currentDay = DateTime.Today;
      await context.Interns.ForEachAsync(x =>
      {
        x.State = eInternState.Started;
      });
      await context.SaveChangesAsync();
      var oldCount = await context.Attendance.CountAsync();

      var result = await repository.AddInternAttendanceEntryAsync();

      Assert.Equal(oldCount, await context.Attendance.CountAsync() - result);
      var attendance = context.Attendance.AsQueryable();
      Assert.Equal(await attendance.Select(x => x.Id).Distinct().CountAsync(), await attendance.CountAsync());
    }

    [Fact]
    public async Task FlagExcessiveAbsenceAsync_UpdatesAttendanceAlarmState()
    {
      await context.Interns.ForEachAsync(x => x.AttendanceAlarmState = eAttendanceAlarmState.None);
      await context.SaveChangesAsync();

      var result = await repository.FlagExcessiveAbsenceAsync();

      Assert.Equal(await context.Interns.CountAsync(), result);
      await context.Interns.ForEachAsync(x =>
      {
        Assert.Equal<eAttendanceAlarmState>(eAttendanceAlarmState.ExcessiveAbsence, x.AttendanceAlarmState);
      });
    }
  }
}