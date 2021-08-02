using System;
using InternManagement.Api.Enums;

namespace InternManagement.Api.Models
{
  public class Attendance
  {
    public int Id { get; set; }
    public DateTime dateTime { get; set; }
    public eAttendanceType Type { get; set; }
    public int InternId { get; set; }
  }
}