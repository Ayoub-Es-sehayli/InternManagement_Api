using System;
using AutoMapper;
using InternManagement.Api.Dtos;
using InternManagement.Api.Enums;
using InternManagement.Api.Models;

namespace InternManagement.Api.Profiles
{
  public class DashboardProfile : Profile
  {
    public static string GetReason(eFileAlarmState fileState, eAttendanceAlarmState attendanceState)
    {
      bool incompleteFile = (fileState == eFileAlarmState.IncompleteFile);
      bool excessiveAbsence = (attendanceState == eAttendanceAlarmState.ExcessiveAbsence);
      if (incompleteFile && excessiveAbsence)
      {
        return "Dossier Incomplet | Absence Excessive";
      }
      else if (incompleteFile)
      {
        return "Dossier Incomplet";
      }
      else if (excessiveAbsence)
      {
        return "Absence Excessive";
      }
      return "";
    }
    public DashboardProfile()
    {
      CreateMap<Intern, AlertInternDto>()
        .ForMember(
          dto => dto.FullName,
          opt => opt.MapFrom(intern => intern.FirstName + " " + intern.LastName))
        .ForMember(
          dto => dto.Reason,
          opt => opt.MapFrom(intern => GetReason(intern.FileAlarmState, intern.AttendanceAlarmState)));

      CreateMap<Intern, LatestInternDto>()
        .ForMember(
          dto => dto.FullName,
          opt => opt.MapFrom(intern => intern.FirstName + " " + intern.LastName));

      CreateMap<Intern, FinishingInternDto>()
        .ForMember(
          dto => dto.FullName,
          opt => opt.MapFrom(intern => intern.FirstName + " " + intern.LastName))
        .ForMember(
          dto => dto.DaysToFinish,
          opt => opt.MapFrom(intern => (intern.EndDate - DateTime.Now).TotalDays)
        );
    }
  }
}