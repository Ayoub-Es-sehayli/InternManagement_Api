using System;
using InternManagement.Api.Enums;

namespace InternManagement.Api.Dtos
{
  public class AttendanceDayDto
  {
    public DateTime Date { get; set; }
    public string Type = "is-danger";

    public static string GetType(eAttendanceType type)
    {
      switch (type)
      {
        case eAttendanceType.Absent:
          return "is-danger";
        case eAttendanceType.Enter:
          return "is-info";
        case eAttendanceType.Exit:
          return "is-warn";
      }
      return "";
    }
  }
}