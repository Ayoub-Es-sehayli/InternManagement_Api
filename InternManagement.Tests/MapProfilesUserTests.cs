using System;
using System.Collections.Generic;
using AutoMapper;
using InternManagement.Api.Dtos;
using InternManagement.Api.Enums;
using InternManagement.Api.Models;
using InternManagement.Api.Profiles;
using Xunit;

namespace InternManagement.Tests
{
    public class MapProfilesUserTests
    {
        [Fact]
        public void MapAddUserAsync_ProperData_ReturnsDto()
        {
            var config = new MapperConfiguration(cfg =>
           {
               cfg.AddProfile<UserProfile>();
           });
            var map = config.CreateMapper();
            var model = new User
            {
                LastName = "Tazi",
                FirstName = "Ahmed",
                Email = "ahmed.tazi@gmail.com",
                Role = eUserRole.Supervisor,
                Password = "00000000000000"
            };

            var dto = new UserAddDto
            {
                LastName = model.LastName,
                FirstName = model.FirstName,
                Email = model.Email,
                Role = model.Role,
                Password = model.Password,
            };
            Assert.Matches(dto.Email, map.Map<User>(model).Email);
            Assert.Matches(dto.Password, map.Map<User>(model).Password);
        }
        [Fact]
        public void MapListUserListDto_ReturnDtoList()
        {
            var config = new MapperConfiguration(cfg =>
          {
              cfg.AddProfile<UserProfile>();
          });
            var map = config.CreateMapper();

            var users = new List<User>
            {
                new User{
                    Id = 1,
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
            Assert.Matches(dtos[0].FullName, map.Map<List<UserListDto>>(users)[0].FullName);
        }
    }

}