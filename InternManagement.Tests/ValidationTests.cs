using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using InternManagement.Api.Dtos;
using InternManagement.Api.Enums;
using Xunit;

namespace InternManagement.Tests
{
  public class ValidationTests
  {
    [Fact]
    public void Intern_TryValidateModel_WithMissingValues_ReturnsFalse()
    {
      var dto = new InternDto
      {
        FirstName = default,
        LastName = default,
        Gender = eGender.Male,
        Email = default,
        Phone = default,
        StartDate = new DateTime(2021, 7, 1),
        EndDate = new DateTime(2021, 8, 31),
        DivisionId = -1,
        Documents = new()
        {
          eDocumentState.Missing,
          eDocumentState.Missing,
          eDocumentState.Missing,
          eDocumentState.Missing,
          eDocumentState.Missing,
          eDocumentState.Missing,
        }
      };

      var validationResults = new List<ValidationResult>();
      bool isValid = Validator.TryValidateObject(dto, new System.ComponentModel.DataAnnotations.ValidationContext(dto), validationResults);

      Assert.False(isValid);
      Assert.NotEmpty(validationResults);
      Assert.InRange<int>(validationResults.Count, 4, 6);
    }

    [Fact]
    public void Intern_TryValidateValue_WithImproperDocuments_ReturnsFalse()
    {
      var dto = new InternDto
      {
        FirstName = "Mohamed",
        LastName = "Hariss",
        Gender = eGender.Male,
        Email = "hariss@contoso.com",
        Phone = "0783848837",

        StartDate = new DateTime(2021, 7, 1),
        EndDate = new DateTime(2021, 8, 31),
        DivisionId = 1,
        Documents = new()
        {
          eDocumentState.Missing, // CV
          eDocumentState.Missing, // Lettre
          eDocumentState.Missing, // Insurance
          eDocumentState.Submitted, // Convention
          eDocumentState.Submitted, // Rapport
          eDocumentState.Submitted // Evaluation Form
        }
      };
      var validationResults = new List<ValidationResult>();
      bool isValid = Validator.TryValidateValue(dto.Documents, new System.ComponentModel.DataAnnotations.ValidationContext(dto), validationResults, new List<ValidationAttribute>() {
        new InternDocumentsAttribute()
      });

      Assert.False(isValid);
      Assert.NotEmpty(validationResults);
      Assert.Equal<int>(1, validationResults.Count);
    }
  }
}