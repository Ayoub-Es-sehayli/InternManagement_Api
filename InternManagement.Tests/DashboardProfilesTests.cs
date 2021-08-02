using System;
using System.Collections.Generic;
using AutoMapper;
using InternManagement.Api.Dtos;
using InternManagement.Api.Enums;
using InternManagement.Api.Models;
using InternManagement.Api.Profiles;
using Xunit;

namespace InternManagement.Tests
{
  public class DashboardProfilesTests
  {
    [Fact]
    public void MapAlertInternDto_ReturnsDto()
    {
      var config = new MapperConfiguration(cfg =>
      {
        cfg.AddProfile<DashboardProfile>();
      });
      var mapper = config.CreateMapper();

      var models = new List<Intern>
          {
            new Intern
            {
              Id = 1,
              FirstName = "Mohamed",
              LastName = "Hariss",
              FileAlarmState = eFileAlarmState.IncompleteFile,
              AttendanceAlarmState = eAttendanceAlarmState.ExcessiveAbsence
            },
            new Intern
            {
              Id = 1,
              FirstName = "Mohamed",
              LastName = "Hariss",
              FileAlarmState = eFileAlarmState.None,
              AttendanceAlarmState = eAttendanceAlarmState.None
            }
          };
      var dtos = new List<AlertInternDto>
      {
        new AlertInternDto
        {
          Id = 1,
          FullName = "Mohamed Hariss",
          Reason = "Dossier Incomplet"
        },
        new AlertInternDto
        {
          Id = 1,
          FullName = "Mohamed Hariss",
          Reason = "Absence Excessive"
        }
      };

      var results = mapper.Map<List<AlertInternDto>>(models);

      for (int i = 0; i < results.Count; i++)
      {
        Assert.Matches("Mohamed Hariss", results[i].FullName);
        Assert.Matches(DashboardProfile.GetReason(models[i].FileAlarmState, models[i].AttendanceAlarmState), results[i].Reason);
      }
    }

    [Fact]
    public void MapLatestInternDto_ReturnsDto()
    {
      var config = new MapperConfiguration(cfg =>
      {
        cfg.AddProfile<DashboardProfile>();
      });
      var mapper = config.CreateMapper();

      var models = new List<Intern>
          {
            new Intern
            {
              Id = 1,
              FirstName = "Mohamed",
              LastName = "Hariss",
              StartDate = new DateTime(2021, 9, 2)
            },
            new Intern
            {
              Id = 2,
              FirstName = "Abir",
              LastName = "Othmani",
              StartDate = new DateTime(2021, 12, 1)
            }
          };
      var dtos = new List<LatestInternDto>
      {
        new LatestInternDto
        {
          Id = 1,
          FullName = "Mohamed Hariss",
          StartDate = new DateTime(2021, 9, 2)
        },
        new LatestInternDto
        {
          Id = 2,
          FullName = "Abir Othmani",
          StartDate = new DateTime(2021, 12, 1)
        }
      };

      var results = mapper.Map<List<LatestInternDto>>(models);

      for (int i = 0; i < results.Count; i++)
      {
        Assert.Matches(dtos[i].FullName, results[i].FullName);
        Assert.Equal<DateTime>(dtos[i].StartDate, results[i].StartDate);
      }
    }


  }
}