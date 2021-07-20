using AutoMapper;
using InternManagement.Api.Dtos;
using InternManagement.Api.Models;

namespace InternManagement.Api.Profiles
{
  public class InternProfile : Profile
  {
    public InternProfile()
    {
      CreateMap<InternDto, Intern>();
      CreateMap<Intern, InternDto>();
    }
  }
}