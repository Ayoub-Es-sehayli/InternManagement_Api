using System.Data;
using System.Linq;
using System.Threading.Tasks;
using InternManagement.Api;
using InternManagement.Api.Enums;
using InternManagement.Api.Models;
using InternManagement.Api.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using MySql.Data.MySqlClient;
using Xunit;

namespace InternManagement.Tests
{
  public class InternRepositoryTests
  {
    private InternRepository repository;

    public InternRepositoryTests()
    {
      var context = new MockDbSeed("InternDB").context;
      this.repository = new InternRepository(context);
    }
    void RemoveInterns(InternContext context)
    {
      foreach (var id in context.Interns.Select(intern => intern.Id))
      {
        var entity = new Intern { Id = id };
        context.Interns.Attach(entity);
        context.Interns.Remove(entity);
      }
      context.SaveChanges();
    }
    [Fact]
    public async Task ConnectingToDb_DoesNotThrowConnectionException()
    {
      var config = new ConfigurationBuilder()
        .AddUserSecrets<ConnectionConfig>()
        .Build();

      var connConfig = config.GetSection("MysqlConnection").Get<ConnectionConfig>();
      var connBuilder = new MySqlConnectionStringBuilder();
      connBuilder.Server = connConfig.Server;
      connBuilder.Database = connConfig.Database;
      connBuilder.Password = connConfig.Password;
      connBuilder.UserID = "InternAdmin";

      var options = new DbContextOptionsBuilder<InternContext>()
       .UseMySQL(connBuilder.ConnectionString)
       .Options;
      var context = new InternContext(options);
      // RemoveInterns(context);
      var count = await context.Interns.CountAsync();
      Assert.NotNull(count);
    }

    [Fact]
    public async Task AddInternAsync_WithProperData_ReturnsAddedObject()
    {
      var oldCount = await repository.GetInternCountAsync();
      var model = new Intern
      {
        FirstName = "Mohamed",
        LastName = "Hariss",
        Gender = eGender.Male,
        Email = "hariss@contoso.com",
        Phone = "0783848837",
        DivisionId = 1,

        Documents = new()
        {
          CV = eDocumentState.Submitted,
          Letter = eDocumentState.Submitted,
          Insurance = eDocumentState.Submitted,
          Convention = eDocumentState.Missing,
          Report = eDocumentState.Missing,
          EvaluationForm = eDocumentState.Missing,
        }
      };
      await repository.AddInternAsync(model);
      Assert.NotEqual<int>(oldCount, await repository.GetInternCountAsync());
    }

    [Fact]
    public async Task InternExistsAsync_WithProperId_ReturnsTrue()
    {
      var latestId = 4;
      var result = await repository.InternExistsAsync(latestId);
      Assert.True(result);
    }

    [Fact]
    async Task InternExistsAsync_WithInvalidId_ReturnsFalse()
    {
      var result = await repository.InternExistsAsync(3001);
      Assert.False(result);
    }

    [Fact]
    public async Task GetInternAsync_WithProperId_ReturnsIntern()
    {
      var latestId = 4;
      var intern = await repository.GetInternAsync(latestId);
      Assert.NotNull(intern);
      Assert.Equal<int>(latestId, intern.Id);
      Assert.NotEmpty(intern.FirstName);
    }

    [Fact]
    public async Task GetInternAsync_WithInvalidId_ReturnNull()
    {
      var intern = await repository.GetInternAsync(3001);
      Assert.Null(intern);
    }

    [Fact]
    public async Task GetInternsAsync_ReturnsList()
    {
      var count = await repository.GetInternCountAsync();
      var interns = await repository.GetInternsAsync();

      Assert.NotNull(interns);
      if (count > 0)
      {
        Assert.NotEmpty(interns);
        Assert.All(interns, intern =>
        {
          Assert.NotEmpty(intern.Division.Name);
          Assert.Null(intern.Email);
          Assert.Null(intern.Documents);
          Assert.Equal(1, intern.StartDate.Year);
          Assert.Equal(1, intern.EndDate.Year);
        });
      }
      else
      {
        Assert.Empty(interns);
      }
    }
  }
}