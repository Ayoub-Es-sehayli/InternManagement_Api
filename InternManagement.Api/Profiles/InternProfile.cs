using System.Collections.Generic;
using AutoMapper;
using InternManagement.Api.Dtos;
using InternManagement.Api.Enums;
using InternManagement.Api.Models;

namespace InternManagement.Api.Profiles
{
  public class InternProfile : Profile
  {
    public InternProfile()
    {
      CreateMap<InternDto, Intern>();
      CreateMap<Intern, InternDto>()
        .ForMember(dto => dto.Documents, opt => opt.MapFrom(intern => new List<eDocumentState>
        {
          intern.Documents.CV,
          intern.Documents.Letter,
          intern.Documents.Insurance,
          intern.Documents.Convention,
          intern.Documents.Report,
          intern.Documents.EvaluationForm
        }));

      CreateMap<Attendance, AttendanceDayDto>()
        .ForMember(dto => dto.Date, opt => opt.MapFrom(a => a.time))
        .ForMember(dto => dto.Type, opt => opt.MapFrom(a => AttendanceDayDto.GetType(a.Type)));

      CreateMap<Intern, InternInfoDto>()
        .ForMember(dto => dto.AttendanceDays, opt => opt.MapFrom(intern => intern.Attendance))
        .ForMember(dto => dto.FullName, opt => opt.MapFrom(intern => intern.FirstName + " " + intern.LastName))
        .ForMember(dto => dto.Division, opt => opt.MapFrom(intern => intern.Division.Name))
        .ForMember(dto => dto.Decision, opt =>
        {
          opt.MapFrom(intern => intern.Decision.Code + "/" + intern.Decision.Date.Year);
          opt.NullSubstitute("-------");
        })
        .ForMember(dto => dto.Documents, opt => opt.MapFrom(intern => new List<eDocumentState>
        {
          intern.Documents.CV,
          intern.Documents.Letter,
          intern.Documents.Insurance,
          intern.Documents.Convention,
          intern.Documents.Report,
          intern.Documents.EvaluationForm
        }));

      CreateMap<Intern, InternListItemDto>()
        .ForMember(dto => dto.FullName, opt => opt.MapFrom(intern => intern.FirstName + " " + intern.LastName))
        .ForMember(dto => dto.Decision, opt => opt.MapFrom(intern => intern.Decision.Code + "/" + intern.Decision.Date.Year))
        .ForMember(dto => dto.Division, opt => opt.MapFrom(intern => intern.Division.Name));

      // * Printing Maps
      CreateMap<Intern, DecisionDto>()
        .ForMember(dto => dto.FullName, opt => opt.MapFrom(intern => intern.FirstName + " " + intern.LastName))
        .ForMember(dto => dto.Department, opt => opt.MapFrom(intern => intern.Division.Department.Name));
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