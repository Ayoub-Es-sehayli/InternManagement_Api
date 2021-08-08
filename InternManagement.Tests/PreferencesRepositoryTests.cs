using System.Threading.Tasks;
using InternManagement.Api.Models;
using InternManagement.Api.Repository;
using Xunit;

namespace InternManagement.Tests
{
    public class PreferencesRepositoryTests
    {
        [Fact]
        public async Task EditPreferencesAsync_ProperData()
        {
            var mockdb = new MockDbSeed("AttendanceList");
            var context = mockdb.context;
            var repository = new PreferencesRepository(context);
            var configuration = new Preferences
            {
                nInterns = 4,
                nAttendanceDays = 5
            };
            var result = repository.EditPreferencesAsync(configuration);
            await repository.SaveChangesAsync();
            Assert.NotNull(result);

        }
    }
}