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
  public class UiServiceTests
  {
    private readonly Mock<IMapper> mapper = new();
    private readonly Mock<IUiRepository> repository = new();
    [Fact]
    public async Task GetDepartmentList_ReturnsDto()
    {
      var models = new List<Department>
      { };
      var dtos = new List<DepartmentDto>
      { };

      repository.Setup(repo => repo.GetDepartmentsAsync().Result).Returns(models);
      mapper.Setup(map => map.Map<IEnumerable<DepartmentDto>>(models)).Returns(dtos);

      var service = new UiService(repository.Object, mapper.Object);

      var results = await service.GetDepartmentDtosAsync();

      Assert.NotNull(results);
    }
  }
}