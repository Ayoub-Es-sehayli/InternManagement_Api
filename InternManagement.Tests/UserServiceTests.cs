using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoMapper;
using InternManagement.Api.Dtos;
using InternManagement.Api.Enums;
using InternManagement.Api.Models;
using InternManagement.Api.Repository;
using InternManagement.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace InternManagement.Tests
{   
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> userRepositoryStub = new();
        private readonly Mock<IMapper> mapper = new();
        private readonly Mock<ILogger> loggerStub = new();
        [Fact]
        public async Task AddUserAsync_ProperData_ReturnsDto()
        {
        var dto = new UserDto
        {
            LastName = "Tazi",
            FirstName = "Ahmed",
            EmailAddress = "ahmed.tazi@gmail.com",
            Roles = new()
            {
                eUserRole.Supervisor,
            },          
            Password = "00000000000000"
        };

        var model = new User
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.EmailAddress,
            Role = eUserRole.Supervisor,
            Password = dto.Password
        };

        mapper.Setup(map => map.Map<User>(dto)).Returns(model);

        model.Id = 1;
        userRepositoryStub.Setup(repo => repo.AddUserAsync(model).Result).Returns(model);

        var service = new UserService(userRepositoryStub.Object, mapper.Object);
        var result = await service.AddUserAsync(dto);
        result = Assert.IsType<UserDto>(result);
        Assert.Equal<string>(model.Email ,result.EmailAddress);
        }
    }
}