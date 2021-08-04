using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using InternManagement.Api.Dtos;
using InternManagement.Api.Enums;
using Xunit;

namespace InternManagement.Tests
{
    public class UserValidationTests
    {
        [Fact]
        public void User_ValidationModel_ProperData_ReturnsFalse()
        {
            var Dto = new UserDto
            {
                FirstName = default,
                LastName = default,
                Email = default,
                Role = eUserRole.Supervisor,
                Password = default
            };

            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(Dto, new System.ComponentModel.DataAnnotations.ValidationContext(Dto), validationResults);

            Assert.False(isValid);
            Assert.NotEmpty(validationResults);
        }
    }
}
