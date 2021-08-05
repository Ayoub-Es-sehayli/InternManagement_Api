using System.Threading.Tasks;
using InternManagement.Api;
using InternManagement.Api.Models;
using InternManagement.Api.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Xunit;

namespace InternManagement.Tests
{
  public class DashboardRepositoryTests
  {
    private DashboardRepository repository;

    public DashboardRepositoryTests()
    {
      var context = new MockDbSeed("dashboardDB").context;
      this.repository = new DashboardRepository(context);
    }
    [Fact]
    public async Task GetLatestInterns_ReturnsList()
    {
      var interns = await repository.GetLatestInternsAsync();

      Assert.NotNull(interns);
    }

    [Fact]
    public async Task GetAbsenteeCountAsync_ReturnsInt()
    {
      var count = await repository.GetAbsenteeCountAsync();

      Assert.NotNull(count);
    }

    [Fact]
    public async Task GetReadyToFinishCountAsync_ReturnsInt()
    {
      var count = await repository.GetReadyToFinishCountAsync();

      Assert.NotNull(count);
    }

    [Fact]
    public async Task GetActiveInternsCountAsync_ReturnsInt()
    {
      var count = await repository.GetActiveInternsCountAsync();

      Assert.NotNull(count);
    }

    [Fact]
    public async Task GetAlertInterns_ReturnsList()
    {
      var interns = await repository.GetAlertInternsAsync();

      Assert.NotNull(interns);
    }

    [Fact]
    public async Task GetFinishingInterns_ReturnsList()
    {
      var interns = await repository.GetFinishingInternsAsync();

      Assert.NotNull(interns);
    }
  }
}