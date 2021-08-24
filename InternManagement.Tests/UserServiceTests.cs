using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using InternManagement.Api;
using InternManagement.Api.Dtos;
using InternManagement.Api.Enums;
using InternManagement.Api.Models;
using InternManagement.Api.Repository;
using InternManagement.Api.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace InternManagement.Tests
{
  public class UserServiceTests
  {
    private readonly Mock<IUserRepository> userRepositoryStub = new();
    private readonly Mock<IMapper> mapper = new();
    private readonly Mock<IConfiguration> configStub = new();
    [Fact]
    public async Task AddUserAsync_ProperData_ReturnsDto()
    {
      var indto = new UserAddDto
      {
        LastName = "Tazi",
        FirstName = "Ahmed",
        Email = "ahmed.tazi@gmail.com",
        Role = eUserRole.Supervisor,
        Password = "00000000000000"
      };

      var model = new User
      {
        FirstName = indto.FirstName,
        LastName = indto.LastName,
        Email = indto.Email,
        Role = eUserRole.Supervisor,
        Password = indto.Password
      };
      var outdto = new UserDto
      {
        FirstName = indto.FirstName,
        LastName = indto.LastName,
        Email = indto.Email,
        Role = eUserRole.Supervisor
      };

      mapper.Setup(map => map.Map<User>(indto)).Returns(model);
      mapper.Setup(map => map.Map<UserDto>(model)).Returns(outdto);
      var confSection = new Mock<IConfigurationSection>();
      var jwtConfig = new JwtConfig
      {
        Salt = "Omrane",
        Issuer = "",
        Audience = ""
      };

      configStub.Setup(config => config.GetSection("Jwt")).Returns(confSection.Object);

      model.Id = 1;
      userRepositoryStub.Setup(repo => repo.AddUserAsync(model).Result).Returns(model);

      var service = new UserService(userRepositoryStub.Object, mapper.Object, configStub.Object);
      var result = await service.AddUserAsync(indto);
      Assert.Equal<string>(model.Email, result.Email);
    }
    [Fact]
    public async Task GetUsersAsync_ReturnDtoList()
    {
      var users = new List<User>
            {
                new User{
                    LastName = "Tazi",
                    FirstName = "Ahmed",
                    Email = "ahmed.tazi@gmail.com",
                    Role = eUserRole.Supervisor,
                    Password = "00000000000000"
                }
            };
      var dtos = new List<UserListDto>
        {
          new UserListDto
          {
            Id = 1,
            FullName = "Ahmed Tazi",
            Email = "ahmed.tazi@gmail.com"
          }
        };
      userRepositoryStub.Setup(repo => repo.GetUsersAsync().Result).Returns(users);
      mapper.Setup(map => map.Map<IEnumerable<UserListDto>>(users)).Returns(dtos);
      var confSection = new Mock<IConfigurationSection>();
      var jwtConfig = new JwtConfig
      {
        Salt = "Omrane",
        Issuer = "",
        Audience = ""
      };

      configStub.Setup(config => config.GetSection("Jwt")).Returns(confSection.Object);

      var service = new UserService(userRepositoryStub.Object, mapper.Object, configStub.Object);
      var result = await service.GetUsersAsync();

      Assert.NotNull(result);
      Assert.NotEmpty(result);
    }
    [Fact]
    public async Task DeleteUserAsync_ProperData()
    {
      var user = new User
      {
        Id = 5,
        LastName = "Tazi",
        FirstName = "Ahmed",
        Email = "ahmed.tazi@gmail.com",
        Role = eUserRole.Supervisor,
        Password = "00000000000000"
      };
      userRepositoryStub.Setup(repo => repo.DeleteUserAsync(user.Id).Result).Returns(user);
      var confSection = new Mock<IConfigurationSection>();
      var jwtConfig = new JwtConfig
      {
        Salt = "Omrane",
        Issuer = "",
        Audience = ""
      };

      configStub.Setup(config => config.GetSection("Jwt")).Returns(confSection.Object);

      var service = new UserService(userRepositoryStub.Object, mapper.Object, configStub.Object);
      var result = await service.DeleteUserAsync(user.Id);

      Assert.NotNull(result);
    }
    [Fact]
    public async Task EditUserAsync_ProperData()
    {
      var model = new User
      {
        Id = 99,
        LastName = "Tazi",
        FirstName = "Ahmed",
        Email = "ahmed.tazi@gmail.com",
        Role = eUserRole.Supervisor,
      };

      var user = new UserDto
      {
        FirstName = model.FirstName,
        LastName = model.LastName,
        Email = model.Email,
        Role = eUserRole.Supervisor,
      };

      var id = model.Id;
      userRepositoryStub.Setup(repo => repo.EditUserAsync(model.Id, model).Result).Returns(model);
      mapper.Setup(map => map.Map<User>(user)).Returns(model);
      mapper.Setup(map => map.Map<UserDto>(model)).Returns(user);

      var confSection = new Mock<IConfigurationSection>();
      var jwtConfig = new JwtConfig
      {
        Salt = "Omrane",
        Issuer = "",
        Audience = ""
      };

      configStub.Setup(config => config.GetSection("Jwt")).Returns(confSection.Object);
      var service = new UserService(userRepositoryStub.Object, mapper.Object, configStub.Object);
      var result = await service.EditUserAsync(id, user);
      Assert.NotNull(result);
    }
  }
}
