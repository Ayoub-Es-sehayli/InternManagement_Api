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
            var count = await context.Interns.CountAsync();
            Assert.NotNull(count);
        }

        [Fact]
        public async Task AddInternAsync_WithProperData_ReturnsAddedObject()
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
            var repository = new InternRepository(context);

            var model = new Intern
            {
                FirstName = "Mohamed",
                LastName = "Hariss",
                Gender = eGender.Male,
                Email = "hariss@contoso.com",
                Phone = "0783848837",

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
            Assert.Equal<int>(1, await repository.GetInternCountAsync());
        }
    }
}