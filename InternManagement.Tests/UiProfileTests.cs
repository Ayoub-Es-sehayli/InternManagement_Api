using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using InternManagement.Api.Dtos;
using InternManagement.Api.Models;
using InternManagement.Api.Profiles;
using Xunit;
namespace InternManagement.Tests
{
  public class UiProfileTests
  {
    [Fact]
    public void MapDivisionDto_ReturnsDto()
    {
      var config = new MapperConfiguration(cfg =>
      {
        cfg.AddProfile<UiProfile>();
      });

      var mapper = config.CreateMapper();

      var model = new Division
      {
        Id = 1,
        Name = "RH"
      };
      var dto = new DivisionDto
      {
        Id = 1,
        Name = "RH"
      };

      var result = mapper.Map<DivisionDto>(model);

      Assert.Equal(dto.Id, result.Id);
      Assert.Matches(dto.Name, result.Name);
    }

    [Fact]
    public void MapDepartmentDto_ReturnsDto()
    {
      var config = new MapperConfiguration(cfg =>
      {
        cfg.AddProfile<UiProfile>();
      });

      var mapper = config.CreateMapper();

      var model = new Department
      {
        Id = 1,
        Name = "RH",
        Divisions = new List<Division>
        {
          new Division
          {
            Id = 1,
            Name = "Development"
          },
          new Division
          {
            Id = 2,
            Name = "Congés"
          }
        }
      };
      var dto = new DepartmentDto
      {
        Id = 1,
        Name = "RH",
        Divisions = new List<DivisionDto>
        {
          new DivisionDto
          {
            Id = 1,
            Name = "Development"
          },
          new DivisionDto
          {
            Id = 2,
            Name = "Congés"
          }
        }
      };

      var result = mapper.Map<DepartmentDto>(model);

      Assert.Equal(dto.Id, result.Id);
      Assert.Matches(dto.Name, result.Name);

    }
  }
}