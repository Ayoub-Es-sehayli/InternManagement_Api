using System.ComponentModel.DataAnnotations;
using InternManagement.Api.Enums;

namespace InternManagement.Api.Dtos
{
  public class UserAddDto : UserDto
  {
    [UserRolesAttribute]
    [Required]
    [EnumDataType(typeof(eUserRole))]
    public eUserRole Role { get; set; }
    [Required]
    [MinLength(6)]
    [DataType(DataType.Password)]
    public string Password { get; set; }
  }
}