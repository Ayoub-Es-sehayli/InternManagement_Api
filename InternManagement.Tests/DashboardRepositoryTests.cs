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
      var config = new ConfigurationBuilder()
        .AddUserSecrets<ConnectionConfig>()
        .Build();

      var connConfig = config.GetSection("MysqlConnection").Get<ConnectionConfig>();
      var connectionString = new MySqlConnectionStringBuilder
      {
        Server = connConfig.Server,
        Database = connConfig.Database,
        Password = connConfig.Password,
        UserID = "InternAdmin"
      }.ConnectionString;

      var options = new DbContextOptionsBuilder<InternContext>()
       .UseMySQL(connectionString)
       .Options;
      var context = new InternContext(options);
      this.repository = new DashboardRepository(context);
    }
    [Fact]
    public async Task GetLatestInterns_ReturnsList()
    {
      var interns = await repository.GetLatestInternsAsync();

      Assert.NotNull(interns);
    }


    [Fact]
    public async Task GetAlertInterns_ReturnsList()
    {
      var interns = await repository.GetAlertInternsAsync();

      Assert.NotNull(interns);
    }
  }
}