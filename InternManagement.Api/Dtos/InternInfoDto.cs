using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using InternManagement.Api.Enums;

namespace InternManagement.Api.Dtos
{
  public class InternInfoDto
  {
    public int Id { get; set; } = -1;
    public string FirstName { get; set; }

    public string LastName { get; set; }

    [EnumDataType(typeof(eGender))]
    public eGender Gender { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    [Required]
    public string Division { get; set; }

    public string Responsable { get; set; }

    [InternDocumentsAttribute]
    public List<eDocumentState> Documents { get; set; }
    public eInternState State { get; set; }
  }
}