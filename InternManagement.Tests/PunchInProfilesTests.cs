using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using InternManagement.Api.Dtos;
using InternManagement.Api.Enums;
using InternManagement.Api.Models;
using InternManagement.Api.Profiles;
using Xunit;

namespace InternManagement.Tests
{
  public class PunchInProfilesTests
  {
    [Fact]
    public void MapAttendance_ReturnsDto()
    {
      //Given
      var config = new MapperConfiguration(cfg =>
      {
        cfg.AddProfile<PunchInProfile>();
      });
      var mapper = config.CreateMapper();

      var model = new Attendance
      {
        date = DateTime.Today,
        time = DateTime.Now,
        InternId = 4
      };

      //When

      var dto = new PunchInDto
      {
        dateTime = model.time,
        InternId = 4
      };
      var result = mapper.Map<Attendance>(dto);
      //Then

      Assert.Equal(model.date, result.date);
      Assert.Equal(model.time, result.time);
      Assert.Equal(model.InternId, result.InternId);
    }

    [Fact]
    public void MapAttendanceDtoList_ReturnsDtoList()
    {
      var config = new MapperConfiguration(cfg =>
      {
        cfg.AddProfile<PunchInProfile>();
      });
      var mapper = config.CreateMapper();
      var currentTime = DateTime.Now;
      var data = new List<Intern>
      {
        new Intern
        {
          Id = 4,
          FirstName = "Mohamed",
          LastName =  "Hariss",
          Attendance = new List<Attendance>
          {
            new Attendance
            {
              Id = 14,
              date = DateTime.Today,
              time = currentTime,
              InternId = 4,
              Type = eAttendanceType.Enter
            }
          },
          State = eInternState.Started
        },
        new Intern
        {
          Id = 5,
          FirstName = "Mohamed",
          LastName =  "Hariss",
          Attendance = new List<Attendance>
          {
            new Attendance
            {
              Id = 13,
              date = DateTime.Today,
              time = currentTime,
              InternId = 4,
              Type = eAttendanceType.Absent
            }
          },
          State = eInternState.Started
        },
        new Intern
        {
          Id = 7,
          FirstName = "Mohamed",
          LastName =  "Hariss",
          Attendance = new List<Attendance>
          {
            new Attendance
            {
              Id = 15,
              date = DateTime.Today,
              time = currentTime,
              InternId = 4,
              Type = eAttendanceType.Enter
            },
            new Attendance
            {
              Id = 15,
              date = DateTime.Today,
              time = currentTime,
              InternId = 4,
              Type = eAttendanceType.Exit
            }
          },
          State = eInternState.Started
        }
      };

      var dtos = new List<AttendanceDto>
      {
        new AttendanceDto
        {
          InternId = 4,
          dateTime = currentTime,
          FullName = "Mohamed Hariss",
          Type = eAttendanceType.Enter
        },
        new AttendanceDto
        {
          InternId = 5,
          dateTime = currentTime,
          FullName = "Mohamed Hariss",
          Type = eAttendanceType.Absent
        },
        new AttendanceDto
        {
          InternId = 7,
          dateTime = currentTime,
          FullName = "Mohamed Hariss",
          Type = eAttendanceType.Exit
        },
      };

      var result = mapper.Map<List<AttendanceDto>>(data);

      for (int i = 0; i < result.Count; i++)
      {
        Assert.Matches(dtos[i].FullName, result[i].FullName);
        Assert.Equal(dtos[i].dateTime, result[i].dateTime);
        Assert.Equal(dtos[i].InternId, result[i].InternId);
        Assert.Equal(dtos[i].Type, result[i].Type);
      }
    }
  }
}