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
              .ForMember(dto => dto.Department, opt => opt.MapFrom(intern => intern.Division.Department.Name))
              .ForMember(dto => dto.Responsable, opt => opt.MapFrom(intern => intern.Responsable));
            CreateMap<Intern, AttestationDto>()
              .ForMember(dto => dto.FullName, opt => opt.MapFrom(intern => intern.FirstName + " " + intern.LastName))
              .ForMember(dto => dto.Location, opt => opt.MapFrom(intern => intern.Division.Department.Location.Name));
            CreateMap<Intern, AnnulationDto>()
              .ForMember(dto => dto.FullName, opt => opt.MapFrom(intern => intern.FirstName + " " + intern.LastName))
              .ForMember(dto => dto.Department, opt => opt.MapFrom(intern => intern.Division.Department.Name))
              .ForMember(dto => dto.DecisionCode, opt => opt.MapFrom(intern => intern.Decision.Code))
              .ForMember(dto => dto.DecisionDate, opt => opt.MapFrom(intern => intern.Decision.Date));
        }
    }
}