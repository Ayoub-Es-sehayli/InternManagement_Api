using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using InternManagement.Api.Enums;

namespace InternManagement.Api.Dtos
{
  public class InternDocumentsAttribute : ValidationAttribute
  {
    protected override ValidationResult IsValid(object Documents, ValidationContext context)
    {
      var documentList = Documents as List<eDocumentState>;
      var docCount = 6;
      if (docCount == documentList.Count)
      {
        if (documentList[0] == eDocumentState.Submitted &&
          documentList[1] == eDocumentState.Submitted &&
          documentList[2] == eDocumentState.Submitted)
        {
          return ValidationResult.Success;
        }
        else
        {
          return new ValidationResult("Document Required to proceed Missing!");
        }
      }
      return new ValidationResult("Documents Required");
    }
  }

  public class InternDto
  {
    public int Id { get; set; } = -1;
    [Required]
    [MinLength(3)]
    [DataType(DataType.Text)]
    public string FirstName { get; set; }

    [Required]
    [MinLength(3)]
    [DataType(DataType.Text)]
    public string LastName { get; set; }

    [Required]
    [EnumDataType(typeof(eGender))]
    public eGender Gender { get; set; }

    [Required]
    [MinLength(10)]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required]
    [MinLength(10)]
    [DataType(DataType.PhoneNumber)]
    public string Phone { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime EndDate { get; set; }

    [Required]
    public int DivisionId { get; set; }

    [Required]
    public string Responsable { get; set; }

    [InternDocumentsAttribute]
    public List<eDocumentState> Documents { get; set; }
  }
}