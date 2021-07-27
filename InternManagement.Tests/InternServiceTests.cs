using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoMapper;
using InternManagement.Api.Dtos;
using InternManagement.Api.Enums;
using InternManagement.Api.Models;
using InternManagement.Api.Repository;
using InternManagement.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace InternManagement.Tests
{
  public class InternServiceTests
  {

    private readonly Mock<IInternRepository> internRepositoryStub = new();
    private readonly Mock<IMapper> mapper = new();
    private readonly Mock<ILogger> loggerStub = new();
    [Fact]
    public async Task AddInternAsync_WithProperData_ReturnsDto()
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
          eDocumentState.Submitted,
          eDocumentState.Submitted,
          eDocumentState.Submitted,
          eDocumentState.Missing,
          eDocumentState.Missing,
          eDocumentState.Missing,
        }
      };

      var model = new Intern
      {
        FirstName = dto.FirstName,
        LastName = dto.LastName,
        Gender = dto.Gender,
        Email = dto.Email,
        Phone = dto.Phone,
        DivisionId = dto.DivisionId,

        Documents = new()
        {
          CV = eDocumentState.Submitted,
          Letter = eDocumentState.Submitted,
          Insurance = eDocumentState.Submitted,
          Convention = eDocumentState.Missing,
          Report = eDocumentState.Missing,
          EvaluationForm = eDocumentState.Missing,
        }
      };

      mapper.Setup(map => map.Map<Intern>(dto)).Returns(model);

      model.Id = 200;
      internRepositoryStub.Setup(repo => repo.AddInternAsync(model).Result).Returns(model);

      var service = new InternService(internRepositoryStub.Object, mapper.Object);
      var result = await service.AddInternAsync(dto);
      result = Assert.IsType<InternDto>(result);
      Assert.Equal<int>(model.Id, result.Id);
    }
  }
}