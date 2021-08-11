using System.Threading.Tasks;
using InternManagement.Api.Repository;
using Xunit;

namespace InternManagement.Tests
{
  public class UiRepositoryTests
  {
    [Fact]
    public async Task GetDepartmentsAsync_ReturnsModels()
    {
      var context = new MockDbSeed("GetDepartments").context;
      IUiRepository repository = new UiRepository(context);

      var models = await repository.GetDepartmentsAsync();

      Assert.NotNull(models);
      Assert.NotEmpty(models);
      Assert.All(models, d => Assert.NotEmpty(d.Divisions));
    }
  }
}