using System;
using AutoMapper;
using InternManagement.Api.Dtos;
using InternManagement.Api.Enums;
using InternManagement.Api.Models;
using InternManagement.Api.Profiles;
using Xunit;

namespace InternManagement.Tests
{
    public class MapProfilesUserTests{

    [Fact]
    public void MapAddUserAsync_ProperData_ReturnsDto(){
      var config = new MapperConfiguration( cfg => 
      {
          cfg.AddProfile<UserProfile>();          
      });
      var map = config.CreateMapper();
      var model = new User{
          LastName = "Tazi",
          FirstName = "Ahmed",
          Email = "ahmed.tazi@gmail.com",
          Role = eUserRole.Supervisor,          
          Password = "00000000000000"
      };

      var Dto = new UserDto {
          LastName = model.LastName,
          FirstName = model.FirstName,
          EmailAddress = model.Email,
          Roles = new() 
              {
                model.Role
              },
          Password = model.Password,
      };
      Assert.Equal<string>(Dto.EmailAddress,map.Map<User>(model).Email); 
      Assert.Equal<string>(Dto.Password,map.Map<UserDto>(model).Password); 
    }
    }
}