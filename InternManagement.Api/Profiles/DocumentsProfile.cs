using System.Collections.Generic;
using AutoMapper;
using InternManagement.Api.Dtos;
using InternManagement.Api.Enums;
using InternManagement.Api.Models;

namespace InternManagement.Api.Profiles
{
  public class DocumentsProfile : Profile
  {
    public DocumentsProfile()
    {
      CreateMap<List<eDocumentState>, Documents>()
        .ForMember(dest => dest.CV, opt => opt.MapFrom(src => src[0]))
        .ForMember(dest => dest.Letter, opt => opt.MapFrom(src => src[1]))
        .ForMember(dest => dest.Insurance, opt => opt.MapFrom(src => src[2]))
        .ForMember(dest => dest.Convention, opt => opt.MapFrom(src => src[3]))
        .ForMember(dest => dest.Report, opt => opt.MapFrom(src => src[4]))
        .ForMember(dest => dest.EvaluationForm, opt => opt.MapFrom(src => src[5]));

      CreateMap<DecisionFormDto, Decision>()
        .ForMember(dto => dto.Id, opt => opt.MapFrom(model => model.InternId));
      CreateMap<AttestationFormDto, Attestation>()
        .ForMember(dto => dto.Id, opt => opt.MapFrom(model => model.InternId));
    }
  }
}