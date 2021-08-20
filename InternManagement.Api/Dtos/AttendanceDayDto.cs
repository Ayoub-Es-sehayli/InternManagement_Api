using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization.Json;
using InternManagement.Api.Enums;

namespace InternManagement.Api.Dtos
{
  public class AttendanceDayDto
  {
    public DateTime Date { get; set; }
    public string Type { get; set; } = "is-danger";

    public static string GetType(eAttendanceType type)
    {
      switch (type)
      {
        case eAttendanceType.Absent:
          return "is-danger";
        case eAttendanceType.Enter:
          return "is-info";
        case eAttendanceType.Exit:
          return "is-warning";
      }
      return "";
    }
  }
}