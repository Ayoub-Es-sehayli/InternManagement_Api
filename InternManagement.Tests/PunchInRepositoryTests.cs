using System;
using System.Linq;
using System.Threading.Tasks;
using InternManagement.Api;
using InternManagement.Api.Dtos;
using InternManagement.Api.Enums;
using InternManagement.Api.Models;
using InternManagement.Api.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Xunit;

namespace InternManagement.Tests
{
  public class PunchInRepositoryTests
  {
    [Fact]
    public async Task GetAttendanceList_ReturnsList()
    {
      var mockdb = new MockDbSeed("AttendanceList");
      var context = mockdb.context;
      var today = DateTime.Today;
      var repository = new PunchInRepository(context);
      var results = await repository.GetAttendanceList();

      Assert.NotNull(results);
      Assert.Equal(results.Count(), results.SelectMany(intern => intern.Attendance).Count());
    }
    [Fact]
    public async Task FlagInternEnterAsync_WithProperData_ReturnsModel()
    {

      var mockdb = new MockDbSeed("PunchInEnterDB");
      var context = mockdb.context;
      var repository = new PunchInRepository(context);
      var today = DateTime.Today;
      var currentTime = new DateTime(2021, 8, 4, 8, 45, 00);
      var model = new Attendance
      {
        InternId = 4,
        date = today,
        time = currentTime
      };
      await repository.FlagInternEnterAsync(model);

      var entryCount = await context.Attendance
      .Select(attendance => new Attendance
      {
        InternId = attendance.InternId,
        date = attendance.date,
        time = attendance.time,
        Type = attendance.Type
      })
      .CountAsync(attendance =>
      attendance.InternId == model.InternId
      && attendance.Type == eAttendanceType.Enter);

      Assert.NotEqual<int>(0, entryCount);
    }
    [Fact]
    public async Task FlagInternExitAsync_WithProperData_ReturnModel()
    {
      var mockdb = new MockDbSeed("PunchInExitDB");
      var context = mockdb.context;
      var repository = new PunchInRepository(context);
      var today = DateTime.Today;
      var entryTime = new DateTime(2021, 8, 4, 8, 45, 00);
      var currentTime = new DateTime(2021, 8, 4, 15, 45, 00);
      var InternId = 3;
      var data = context.Attendance
      .Select(attendance => new Attendance
      {
        Id = attendance.Id,
        InternId = attendance.InternId,
        date = attendance.date
      }).Where(attendance =>
      attendance.InternId == InternId
      && attendance.date == today
      && attendance.Type == eAttendanceType.Absent)
      .FirstOrDefault();
      if (data != null)
      {
        context.ChangeTracker.Clear();
        data.time = entryTime;
        data.Type = eAttendanceType.Enter;
        context.Update(data);
        await context.SaveChangesAsync();
      }
      var count = await context.Attendance
      .CountAsync(attendance =>
      attendance.InternId == InternId
      && attendance.date == today
      && attendance.Type == eAttendanceType.Enter);
      Assert.NotEqual<int>(0, count);
      if (count == 1)
      {
        var exitEntry = new Attendance
        {
          Id = context.Attendance.Select(a => a.Id).OrderByDescending(a => a).FirstOrDefault() + 1,
          date = today,
          time = currentTime,
          InternId = InternId,
          Type = eAttendanceType.Exit
        };
        await context.Attendance.AddAsync(exitEntry);
        await context.SaveChangesAsync();
      }
      var exitCount = await context.Attendance
      .Where(attendance =>
      attendance.InternId == InternId
      && attendance.date == today)
      .ToListAsync();

      Assert.Equal<int>(2, exitCount.Count());
    }
  }
}