using System;
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
    private readonly InternContext context;
    private InternRepository repository;

    public InternRepositoryTests()
    {
      this.context = new MockDbSeed("InternDB").context;
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
          Assert.Null(intern.Email);
          Assert.Null(intern.Documents);
          Assert.NotEmpty(intern.Division.Name);
          Assert.NotEqual(DateTime.Today.Year, intern.EndDate.Year);
          Assert.Equal(1, intern.StartDate.Year);
        });
      }
      else
      {
        Assert.Empty(interns);
      }
    }

    [Fact]
    public async Task SetDecisionDetails_AddsDecision()
    {
      var id = 201;
      var currentTime = DateTime.Today;

      await context.AddAsync<Intern>(new Intern
      {
        Id = id,
        State = eInternState.ApplicationFilled,
        FirstName = "Mohamed",
        LastName = "Hariss",
        Email = "mohamed.hariss@gmail.com",
        Phone = "0684257139",
        AttendanceAlarmState = eAttendanceAlarmState.None,
        FileAlarmState = eFileAlarmState.None,
        DivisionId = 25,
        Gender = eGender.Male,
        StartDate = DateTime.Today,
        EndDate = DateTime.Today.AddMonths(2),
        Documents = new Documents
        {
          Id = id,
          CV = eDocumentState.Submitted,
          Letter = eDocumentState.Submitted,
          Insurance = eDocumentState.Submitted,
          Convention = eDocumentState.Submitted,
          Report = eDocumentState.Invalid,
          EvaluationForm = eDocumentState.Missing
        }
      });
      await context.SaveChangesAsync();
      var decision = new Decision
      {
        Id = id,
        InternId = id,
        Code = "2548/2021",
        Date = currentTime
      };
      var count = await repository.GetInternCountAsync();
      var oldCount = await context.Set<Decision>().CountAsync();
      Assert.NotEqual(0, count);
      if (await repository.InternExistsAsync(id))
      {
        var intern = await repository.GetInternAsync(id);
        if (intern.Decision == null)
        {
          context.ChangeTracker.Clear();
          await context.AddAsync(decision);
        }
        else
        {
          intern.Decision.Code = decision.Code;
          intern.Decision.Date = decision.Date;
        };
        switch (intern.State)
        {
          case eInternState.ApplicationFilled:
          case eInternState.AssignedDecision:
            intern.State = eInternState.AssignedDecision;
            break;
        }
        await context.SaveChangesAsync();
      }

      var updated = await repository.GetInternAsync(id);

      Assert.NotEqual(oldCount, await context.Set<Decision>().CountAsync());
      Assert.NotNull(updated);
      Assert.NotNull(updated.Decision);
      Assert.Equal(decision.Date, updated.Decision.Date);
      Assert.Matches(decision.Code, updated.Decision.Code);
    }
  }
}