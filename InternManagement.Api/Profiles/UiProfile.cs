using AutoMapper;
using InternManagement.Api.Dtos;
using InternManagement.Api.Models;

namespace InternManagement.Api.Profiles
{
  public class UiProfile : Profile
  {
    public UiProfile()
    {
      CreateMap<Division, DivisionDto>();
      CreateMap<Department, DepartmentDto>();
    }
  }
}