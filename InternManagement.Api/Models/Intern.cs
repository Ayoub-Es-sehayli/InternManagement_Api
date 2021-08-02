using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using InternManagement.Api.Enums;

namespace InternManagement.Api.Models
{
  public class Intern
  {
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }

    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; }

    [DataType(DataType.Date)]
    public DateTime EndDate { get; set; }
    public int DivisionId { get; set; }
    public Division Division { get; set; }

    public Documents Documents { get; set; }

    [EnumDataType(typeof(eGender))]
    public eGender Gender { get; set; }

    [EnumDataType(typeof(eInternState))]
    public eInternState State { get; set; }
    [EnumDataType(typeof(eFileAlarmState))]
    public eFileAlarmState FileAlarmState { get; set; } = eFileAlarmState.None;

    [EnumDataType(typeof(eAttendanceAlarmState))]
    public eAttendanceAlarmState AttendanceAlarmState { get; set; } = eAttendanceAlarmState.None;
    public IEnumerable<Attendance> Attendance { get; internal set; }
  }
}