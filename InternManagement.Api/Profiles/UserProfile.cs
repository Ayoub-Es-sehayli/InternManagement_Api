using AutoMapper;
using InternManagement.Api.Dtos;
using InternManagement.Api.Models;

namespace InternManagement.Api.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile(){
            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>();
        }
    }
}