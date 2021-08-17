using System;
using System.Collections.Generic;
using InternManagement.Api.Enums;

namespace InternManagement.Api.Dtos
{
  public class InternInfoDto
  {
    public int Id { get; set; } = -1;
    public string FullName { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }
    public string Decision { get; set; }
    public string Division { get; set; }

    public string Responsable { get; set; }
    public List<eDocumentState> Documents { get; set; }
    public eInternState State { get; set; }
    public List<AttendanceDayDto> AttendanceDays { get; set; }
  }
}