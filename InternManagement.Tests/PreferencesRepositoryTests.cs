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
  public class PreferencesRepositoryTests
  {
    [Fact]
    public async Task EditPreferencesAsync_ProperData()
    {
      var mockdb = new MockDbSeed("InternDB");
      var context = mockdb.context;
      var repository = new PreferencesRepository(context);
      var configuration = new Preferences
      {
        nInterns = 4,
        nAttendanceDays = 5
      };
      var result = await repository.EditPreferencesAsync(configuration);
      Assert.NotNull(result);

    }
  }
}