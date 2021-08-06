using System.ComponentModel.DataAnnotations;
namespace InternManagement.Api.Dtos
{
    public class UserAddDto : UserDto
    {

        [Required]
        [MinLength(6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}