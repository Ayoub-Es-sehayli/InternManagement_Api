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
              .ForMember(dto => dto.Decision, opt => opt.MapFrom(intern => intern.Decision.Code + "/" + intern.Decision.Date.Year))
              .ForMember(dto => dto.Division, opt => opt.MapFrom(intern => intern.Division.Name));
            CreateMap<Intern, DecisionDto>()
              .ForMember(dto => dto.FullName, opt => opt.MapFrom(intern => intern.FirstName + " " + intern.LastName))
              .ForMember(dto => dto.Department, opt => opt.MapFrom(intern => intern.Division.Department.Name));
        }
    }
}