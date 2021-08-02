using System;
using AutoMapper;
using InternManagement.Api.Dtos;
using InternManagement.Api.Enums;
using InternManagement.Api.Models;

namespace InternManagement.Api.Profiles
{
  public class DashboardProfile : Profile
  {
    public DashboardProfile()
    {
      CreateMap<Intern, LatestInternDto>()
        .ForMember(
          dto => dto.FullName,
          opt => opt.MapFrom(intern => intern.FirstName + " " + intern.LastName));

    }
  }
}