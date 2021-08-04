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
using Microsoft.Extensions.Logging;
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
            var dto = new UserDto
            {
                LastName = "Tazi",
                FirstName = "Ahmed",
                Email = "ahmed.tazi@gmail.com",
                Role = eUserRole.Supervisor,
                Password = "00000000000000"
            };

            var model = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Role = eUserRole.Supervisor,
                Password = dto.Password
            };

            mapper.Setup(map => map.Map<User>(dto)).Returns(model);
            var confSection = new Mock<IConfigurationSection>();
            var jwtConfig = new JwtConfig
            {
                Salt = "Omrane"
            };

            configStub.Setup(config => config.GetSection("JwtKey")).Returns(confSection.Object);

            model.Id = 1;
            userRepositoryStub.Setup(repo => repo.AddUserAsync(model).Result).Returns(model);

            var service = new UserService(userRepositoryStub.Object, mapper.Object, configStub.Object);
            var result = await service.AddUserAsync(dto);
            result = Assert.IsType<UserDto>(result);
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
                Salt = "Omrane"
            };

            configStub.Setup(config => config.GetSection("JwtKey")).Returns(confSection.Object);

            var service = new UserService(userRepositoryStub.Object, mapper.Object, configStub.Object);
            var result = await service.GetUsersAsync();

            Assert.NotNull(result);
            Assert.NotEmpty(result);

        }
    }
}