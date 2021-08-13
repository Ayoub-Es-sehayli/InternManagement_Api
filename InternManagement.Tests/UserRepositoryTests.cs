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
    [Fact]
    public async Task AddUserAsync_ProperData_ReturnsDto()
    {
      var mockdb = new MockDbSeed("AttendanceList");
      var context = mockdb.context;
      var repository = new UserRepository(context);
      var oldCount = await repository.GetUserCountAsync();
      var model = new User
      {
        Id = 99,
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
    public async Task EditUserAsync_ProperData()
    {
      var mockdb = new MockDbSeed("AttendanceList");
      var context = mockdb.context;
      var repository = new UserRepository(context);
      var model = new User
      {
        Id = 99,
        LastName = "Tazi",
        FirstName = "Ahmed",
        Email = "ahmed.tazi@gmail.com",
        Role = eUserRole.Supervisor,
      };
      var user = await repository.GetUserByIdAsync(model.Id);
      if (user != null)
      {
        user.Email = model.Email;
        user.FirstName = model.FirstName;
        user.LastName = model.LastName;
        user.Role = model.Role;
        context.Users.Update(user);
        await repository.SaveChangesAsync();
      }
      var result = repository.GetUsersAsync();
      Assert.NotNull(result);
    }
    [Fact]
    public async Task DeleteUserAsync_ProperData()
    {
      var mockdb = new MockDbSeed("DeleteUser");
      var context = mockdb.context;
      var id = 1;
      context.Users.Add(new User
      {
        Id = id,
        LastName = "Tazi",
        FirstName = "Ahmed",
        Email = "ahmed.tazi@gmail.com",
        Role = eUserRole.Supervisor,
        Password = "00000000000000"
      });
      context.SaveChanges();
      var repository = new UserRepository(context);
      var user = await repository.GetUserByIdAsync(id);
      if (user != null)
      {
        context.Entry(user).State = EntityState.Deleted;
        await context.SaveChangesAsync();
      }
    }
  }
}
