using System;
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
  public class AuthServiceTests
  {
    private readonly Mock<IUserRepository> userRepositoryStub = new();
    private readonly Mock<IMapper> mapper = new();
    [Fact]
    public async Task AuthenticateUser_ProperData_ReturnsUser()
    {
      var authRequest = new AuthenticateRequest
      {
        Username = "wiam.essaadi.2000@gmail.com",
        Password = "wiam1234"
      };

      // var authResponse = new AuthenticateResponse(authRequest, generateJwtToken(authRequest));
      var inMemorySettings = new Dictionary<string, string>
            {
                {"Jwt:Salt", "Omrane Secret Jwt Authentication Key"},
                {"Jwt:Issuer", "http://localhost:5000/"},
                {"Jwt:Audience", "http://localhost:5000/"}
            };

      var user = new User
      {
        Id = 1,
        LastName = "essaadi",
        FirstName = "wiam",
        Role = eUserRole.Admin,
        Email = authRequest.Username,
        Password = authRequest.Password
      };

      IConfiguration config = new ConfigurationBuilder()
      .AddInMemoryCollection(inMemorySettings)
      .Build();

      userRepositoryStub.Setup(repo => repo.UserExistsAsync(authRequest.Username, authRequest.Password).Result).Returns(user);
      var service = new UserService(userRepositoryStub.Object, mapper.Object, config);
      var result = await service.AuthenticateAsync(authRequest);
      Assert.Matches(authRequest.Username, result.Username);
    }
  }
}