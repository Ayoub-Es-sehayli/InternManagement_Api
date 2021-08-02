using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using InternManagement.Api.Dtos;
using InternManagement.Api.Enums;
using InternManagement.Api.Models;
using InternManagement.Api.Repository;
using InternManagement.Api.Services;
using Moq;
using Xunit;

namespace InternManagement.Tests
{
  public class DashboardServiceTests
  {
    private Mock<IDashboardRepository> repositoryStub = new();
    private Mock<IMapper> mapper = new();
    [Fact]
    public async Task GetAlertInternsAsync_ReturnsDtoList()
    {
      var models = new List<Intern>
          {
            new Intern
            {
              Id = 1,
              FirstName = "Mohamed",
              LastName = "Hariss",
             FileAlarmState = eFileAlarmState.IncompleteFile
            },
            new Intern
            {
              Id = 2,
              FirstName = "Mohamed",
              LastName = "Hariss",
              AttendanceAlarmState = eAttendanceAlarmState.ExcessiveAbsence
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
          Id = 2,
          FullName = "Mohamed Hariss",
          Reason = "Absence Excessive"
        },
        new AlertInternDto
        {
          Id = 3,
          FullName = "Mohamed Hariss",
          Reason = "Dossier Incomplet | Absence Excessive"
        }
      };

      repositoryStub.Setup(repo => repo.GetAlertInternsAsync().Result).Returns(models);
      mapper.Setup(map => map.Map<IEnumerable<AlertInternDto>>(models)).Returns(dtos);
      var service = new DashboardService(repositoryStub.Object, mapper.Object);

      var result = await service.GetAlertInternsAsync();
      Assert.NotNull(result);
      Assert.NotEmpty(result);
    }

    [Fact]
    public async Task GetLatestInternsAsync_ReturnsDtoList()
    {
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
          StartDate = new DateTime(2021, 9, 2)
        }
      };

      repositoryStub.Setup(repo => repo.GetLatestInternsAsync().Result).Returns(models);
      mapper.Setup(map => map.Map<IEnumerable<LatestInternDto>>(models)).Returns(dtos);
      var service = new DashboardService(repositoryStub.Object, mapper.Object);

      var result = await service.GetLatestInternsAsync();
      Assert.NotNull(result);
      Assert.NotEmpty(result);
    }
    [Fact]
    public async Task GetFinishingInternsAsync_ReturnsDtoList()
    {
      var models = new List<Intern>
          {
            new Intern
            {
              Id = 1,
              FirstName = "Mohamed",
              LastName = "Hariss",
              EndDate = new DateTime(2021, 8, 6),
            },
            new Intern
            {
              Id = 1,
              FirstName = "Karim",
              LastName = "Morabit",
              EndDate = new DateTime(2021, 8, 6),
            },
            new Intern
            {
              Id = 2,
              FirstName = "Abir",
              LastName = "Othmani",
              EndDate = new DateTime(2021, 8, 5),
            }
          };
      var dtos = new List<FinishingInternDto>
      {
        new FinishingInternDto
        {
          Id = 1,
          FullName = "Mohamed Hariss",
          DaysToFinish = 5
        },
        new FinishingInternDto
        {
          Id = 1,
          FullName = "Karim Morabit",
          DaysToFinish = 5
        },
        new FinishingInternDto
        {
          Id = 2,
          FullName = "Abir Othmani",
          DaysToFinish = 4
        }
      };

      repositoryStub.Setup(repo => repo.GetFinishingInternsAsync().Result).Returns(models);
      mapper.Setup(map => map.Map<IEnumerable<FinishingInternDto>>(models)).Returns(dtos);
      var service = new DashboardService(repositoryStub.Object, mapper.Object);

      var result = await service.GetFinishingInternsAsync();
      Assert.NotNull(result);
      Assert.NotEmpty(result);
    }

  }
}