using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using InternManagement.Api.Dtos;
using InternManagement.Api.Models;
using InternManagement.Api.Repository;
using InternManagement.Api.Services;
using Moq;
using Xunit;

namespace InternManagement.Tests
{
  public class PunchInServiceTests
  {
    private readonly Mock<IPunchInRepository> repository = new();
    private readonly Mock<IMapper> mapper = new();

    [Fact]
    public async Task FlagInternEnterAsync_WithProperData_ReturnsDto()
    {
      var dto = new PunchInDto
      {
        InternId = 4,
        dateTime = new DateTime(2021, 8, 21, 8, 45, 0)
      };
      var model = new Attendance
      {
        InternId = dto.InternId,
        date = dto.dateTime.Date,
        time = dto.dateTime
      };
      mapper.Setup(map => map.Map<Attendance>(dto)).Returns(model);
      var service = new PunchInService(repository.Object, mapper.Object);
      await service.FlagInternEnterAsync(dto);
    }

    [Fact]
    public async Task FlagInternExitAsync_WithProperData_ReturnsDto()
    {
      var dto = new PunchInDto
      {
        InternId = 4,
        dateTime = new DateTime(2021, 8, 21, 8, 45, 0)
      };
      var model = new Attendance
      {
        InternId = dto.InternId,
        date = dto.dateTime.Date,
        time = dto.dateTime
      };
      mapper.Setup(map => map.Map<Attendance>(dto)).Returns(model);
      var service = new PunchInService(repository.Object, mapper.Object);
      await service.FlagInternExitAsync(dto);
    }
    [Fact]

    public async Task GetAttendanceList_ReturnsDtoList()
    {
      var dtos = new List<AttendanceDto>
      { };
      var models = new List<Intern>
      { };

      mapper.Setup(map => map.Map<IEnumerable<AttendanceDto>>(models)).Returns(dtos);
      repository.Setup(repo => repo.GetAttendanceList().Result).Returns(models);

      var service = new PunchInService(repository.Object, mapper.Object);

      var result = await service.GetAttendanceList();

      Assert.NotNull(result);
    }
  }
}