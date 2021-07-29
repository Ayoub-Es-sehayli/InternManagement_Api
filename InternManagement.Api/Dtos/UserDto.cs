using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using InternManagement.Api.Enums;
using InternManagement.Api.Models;

namespace InternManagement.Api.Dtos
{
    public class UserRolesAttribute :ValidationAttribute {
        protected override ValidationResult IsValid(object Roles, ValidationContext context)
        {
            var roleList = Roles as List<eUserRole>;
            var roleCount = 2;
            if (roleCount == roleList.Count)
            {
                if ( roleList[0] == eUserRole.Admin || roleList[1] == eUserRole.Supervisor )
                {
                    return ValidationResult.Success;
                }
                else{
                    return new ValidationResult("Role Required to proceed Missing!");
                }

            }
            return new ValidationResult("Role Required");
        }
    }
    public class UserDto
    {

        public string FirstName { get; set; }
        [Required]
        [MinLength(3)]
        [DataType(DataType.Text)]
        public string LastName { get; set; }
        [Required]
        [MinLength(3)]
        [DataType(DataType.Text)]
        public string EmailAddress { get; set; }
        [Required]
        [MinLength(10)]
        [DataType(DataType.EmailAddress)]
        public string Password { get; set; }
        [Required]
        [MinLength(6)]
        [DataType(DataType.Password)]

        [UserRolesAttribute]
        public List<eUserRole> Roles { get; set; }


    }
}
