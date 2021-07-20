using System;
using AutoMapper;
using InternManagement.Api.Dtos;
using InternManagement.Api.Enums;
using InternManagement.Api.Models;
using InternManagement.Api.Profiles;
using Xunit;

namespace InternManagement.Tests
{
  public class MapProfilesTests
  {
    [Fact]
    public void MapInternDto_WithProperObject_ReturnsInternObject()
    {
      var config = new MapperConfiguration(cfg =>
      {
        cfg.AddProfile<InternProfile>();
      });
      var map = config.CreateMapper();
      var dto = new InternDto
      {
        Id = default,
        FirstName = "Mohamed",
        LastName = "Hariss",
        Gender = eGender.Male,
        Email = "hariss@contoso.com",
        Phone = "0783848837",

        StartDate = new DateTime(2021, 7, 1),
        EndDate = new DateTime(2021, 8, 31),
        DivisionId = 1
      };
      var model = new Intern
      {
        Id = default,
        FirstName = dto.FirstName,
        LastName = dto.LastName,
        Gender = dto.Gender,
        Email = dto.Email,
        Phone = dto.Phone,

        StartDate = new DateTime(2021, 7, 1),
        EndDate = new DateTime(2021, 8, 31),
        DivisionId = 1
      };
      Assert.Equal<string>(model.Email, map.Map<Intern>(dto).Email);
    }

    [Fact]
    public void MapDocumentsList_WithProperList_ReturnsDocuments()
    {
      var config = new MapperConfiguration(cfg =>
      {
        cfg.AddProfile<InternProfile>();
        cfg.AddProfile<DocumentsProfile>();
      });
      var map = config.CreateMapper();
      var dto = new InternDto
      {
        Documents = new()
        {
          eDocumentState.Submitted,
          eDocumentState.Missing,
          eDocumentState.Submitted,
          eDocumentState.Missing,
          eDocumentState.Invalid,
          eDocumentState.Missing,
        }
      };
      var model = new Intern
      {
        Documents = new()
        {
          CV = eDocumentState.Submitted,
          Letter = eDocumentState.Missing,
          Insurance = eDocumentState.Submitted,
          Convention = eDocumentState.Missing,
          Report = eDocumentState.Invalid,
          EvaluationForm = eDocumentState.Missing,
        }
      };
      Assert.StrictEqual<eDocumentState>(model.Documents.CV, map.Map<Intern>(dto).Documents.CV);
    }

    [Fact]
    public void MapIntern_WithProperData_ReturnsInternDto()
    {
      var config = new MapperConfiguration(cfg =>
      {
        cfg.AddProfile<InternProfile>();
      });
      var map = config.CreateMapper();
      var model = new Intern
      {
        Id = default,
        FirstName = "Mohamed",
        LastName = "Hariss",
        Gender = eGender.Male,
        Email = "hariss@contoso.com",
        Phone = "0783848837",
        StartDate = new DateTime(2021, 7, 1),
        EndDate = new DateTime(2021, 8, 31),
        DivisionId = 1
      };
      var dto = new InternDto
      {
        Id = default,
        FirstName = model.FirstName,
        LastName = model.LastName,
        Gender = model.Gender,
        Email = model.Email,
        Phone = model.Phone,

        StartDate = new DateTime(2021, 7, 1),
        EndDate = new DateTime(2021, 8, 31),
        DivisionId = 1
      };
      Assert.Equal<string>(dto.Email, map.Map<InternDto>(model).Email);
    }

    [Fact]
    public void MapDocuments_WithProperData_ReturnsDocumentsList()
    {
      var config = new MapperConfiguration(cfg =>
      {
        cfg.AddProfile<InternProfile>();
        cfg.AddProfile<DocumentsProfile>();
      });
      var map = config.CreateMapper();
      var model = new Intern
      {
        Documents = new()
        {
          CV = eDocumentState.Submitted,
          Letter = eDocumentState.Missing,
          Insurance = eDocumentState.Submitted,
          Convention = eDocumentState.Missing,
          Report = eDocumentState.Invalid,
          EvaluationForm = eDocumentState.Missing,
        }
      };
      var dto = new InternDto
      {
        Documents = new()
        {
          eDocumentState.Submitted,
          eDocumentState.Missing,
          eDocumentState.Submitted,
          eDocumentState.Missing,
          eDocumentState.Invalid,
          eDocumentState.Missing,
        }
      };
      Assert.StrictEqual<eDocumentState>(model.Documents.CV, map.Map<InternDto>(dto).Documents[0]);
    }
  }
}