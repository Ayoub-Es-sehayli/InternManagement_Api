using System;
using InternManagement.Api.Enums;

namespace InternManagement.Api.Models
{
  public class Attendance
  {
    public int Id { get; set; }
    public DateTime date { get; set; }
    public DateTime time { get; set; }
    public eAttendanceType Type { get; set; }
    public int InternId { get; set; }
  }
}