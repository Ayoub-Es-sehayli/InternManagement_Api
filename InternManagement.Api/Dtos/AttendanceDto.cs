using System;
using InternManagement.Api.Enums;

namespace InternManagement.Api.Dtos
{
  public class AttendanceDto
  {
    public int InternId { get; set; }
    public string FullName { get; set; }
    public DateTime dateTime { get; set; }
    public eAttendanceType Type { get; set; }
  }
}