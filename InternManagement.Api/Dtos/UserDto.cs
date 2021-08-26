using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using InternManagement.Api.Enums;
using InternManagement.Api.Models;

namespace InternManagement.Api.Dtos
{
  public class UserRolesAttribute : ValidationAttribute
  {
    protected override ValidationResult IsValid(object Role, ValidationContext context)
    {
      var role = (eUserRole)Role;
      if (role == eUserRole.Admin || role == eUserRole.Supervisor)
      {
        return ValidationResult.Success;
      }
      else
      {
        return new ValidationResult("Role Required to proceed Missing!");
      }
    }
  }
  public class UserDto
  {
    [Required]
    [MinLength(3)]
    [DataType(DataType.Text)]
    public string LastName { get; set; }
    [Required]
    [MinLength(3)]
    [DataType(DataType.Text)]
    public string FirstName { get; set; }
    [Required]
    [MinLength(10)]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [UserRolesAttribute]
    [Required]
    [EnumDataType(typeof(eUserRole))]
    public eUserRole Role { get; set; }


  }
}
