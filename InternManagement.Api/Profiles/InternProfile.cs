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
      CreateMap<Intern, InternListItemDto>()
        .ForMember(dto => dto.FullName, opt => opt.MapFrom(intern => intern.FirstName + " " + intern.LastName))
        .ForMember(dto => dto.Division, opt => opt.MapFrom(intern => intern.Division.Name));
    }
  }
}