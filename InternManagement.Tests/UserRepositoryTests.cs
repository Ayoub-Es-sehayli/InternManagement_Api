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
    public class UserRepositoryTests
    {
        private UserRepository repository;

        public UserRepositoryTests()
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
            this.repository = new UserRepository(context);

        }
        [Fact]
        public async Task AddUserAsync_ProperData_ReturnsDto()
        {
            var oldCount = await repository.GetUserCountAsync();
            var model = new User
            {
                LastName = "Tazi",
                FirstName = "Ahmed",
                Email = "ahmed.tazi@gmail.com",
                Role = eUserRole.Supervisor,
                Password = "00000000000000"
            };
            var result = await repository.AddUserAsync(model);
            Assert.NotNull(result);
            Assert.NotEqual<int>(oldCount, await repository.GetUserCountAsync());
        }
        [Fact]
        public async Task DeleteUserAsync_ProperData()
        {
            var result = await repository.DeleteUserAsync(21);
            Assert.NotNull(result);
        }
    }
}