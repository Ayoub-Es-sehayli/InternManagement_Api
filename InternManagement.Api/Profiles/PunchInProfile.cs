using System.Linq;
using AutoMapper;
using InternManagement.Api.Dtos;
using InternManagement.Api.Models;

namespace InternManagement.Api.Profiles
{
  public class PunchInProfile : Profile
  {
    public PunchInProfile()
    {
      CreateMap<PunchInDto, Attendance>()
        .ForMember(a => a.date, opt => opt.MapFrom(dto => dto.dateTime.Date))
        .ForMember(a => a.time, opt => opt.MapFrom(dto => dto.dateTime));

      CreateMap<Intern, AttendanceDto>()
        .ForMember(dto => dto.InternId, opt => opt.MapFrom(i => i.Id))
        .ForMember(dto => dto.FullName, opt => opt.MapFrom(i => i.FirstName + " " + i.LastName))
        .ForMember(dto => dto.dateTime,
            opt => opt.MapFrom(i => i.Attendance.Last().time))
        .ForMember(dto => dto.Type,
            opt => opt.MapFrom(i => i.Attendance.Last().Type));
    }
  }
}