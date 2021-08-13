using System;
using System.Collections.Generic;
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
      Assert.Matches(model.Email, map.Map<Intern>(dto).Email);
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
      Assert.Matches(dto.Email, map.Map<InternDto>(model).Email);
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

    [Fact]
    public void MapListInternListItemDto_ReturnDtoList()
    {
      var config = new MapperConfiguration(cfg =>
      {
        cfg.AddProfile<InternProfile>();
      });
      var map = config.CreateMapper();

      var interns = new List<Intern>
      {
        new Intern{
          Id = 1,
          FirstName = "Mohamed",
          LastName = "Hariss",
          Division = new Division
          {
            Name = "Division comptabilite et Fiscalite"
          },
          State = eInternState.AssignedDecision
        }
      };

      var dtos = new List<InternListItemDto>
      {
        new InternListItemDto
        {
          Id = 1,
          FullName = "Mohamed Hariss",
          Division = "Division comptabilite et Fiscalite",
          State = eInternState.AssignedDecision
        }
      };
      Assert.Matches(dtos[0].FullName, map.Map<List<InternListItemDto>>(interns)[0].FullName);
    }

    [Fact]
    public void MapDecision_FromDto_ReturnsModel()
    {
      var config = new MapperConfiguration(cfg => cfg.AddProfile<DocumentsProfile>());
      var mapper = config.CreateMapper();

      var currentDate = DateTime.Today;
      var model = new Decision
      {
        Id = 3,
        InternId = 3,
        Code = "1255/2021",
        Date = currentDate
      };
      var dto = new DecisionFormDto
      {
        InternId = model.InternId,
        Code = model.Code,
        Date = model.Date
      };

      var result = mapper.Map<Decision>(dto);
      Assert.Equal(model.Id, result.Id);
      Assert.Equal(model.InternId, result.InternId);
      Assert.Equal(model.Date, result.Date);
      Assert.Matches(model.Code, result.Code);
    }

    [Fact]
    public void MapAttestation_FromDto_ReturnsModel()
    {
      var config = new MapperConfiguration(cfg => cfg.AddProfile<DocumentsProfile>());
      var mapper = config.CreateMapper();
      var currentDate = DateTime.Today;
      var id = 101;
      var model = new Attestation
      {
        Id = id,
        InternId = id,
        Code = "2054/2021",
        Date = currentDate
      };
      var dto = new AttestationFormDto
      {
        InternId = id,
        Code = model.Code,
        Date = model.Date
      };

      var result = mapper.Map<Attestation>(dto);

      Assert.Matches(model.Code, result.Code);
      Assert.Equal(model.Id, result.Id);
      Assert.Equal(model.Date, result.Date);
    }
  }
}