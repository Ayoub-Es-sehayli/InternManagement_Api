using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoMapper;
using InternManagement.Api.Dtos;
using InternManagement.Api.Enums;
using InternManagement.Api.Helpers;
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
        private readonly Mock<IPrintHelper> print = new();
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

            var service = new InternService(internRepositoryStub.Object, mapper.Object, print.Object);
            var result = await service.AddInternAsync(dto);
            result = Assert.IsType<InternDto>(result);
            Assert.Equal<int>(model.Id, result.Id);
        }

        [Fact]
        public async Task GetInternByIdAsync_WithProperData_ReturnsDto()
        {
            var id = 200;
            var dto = new InternDto
            {
                Id = id,
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

            internRepositoryStub.Setup(repo => repo.InternExistsAsync(id).Result).Returns(true);
            internRepositoryStub.Setup(repo => repo.GetInternAsync(id).Result).Returns(model);

            mapper.Setup(map => map.Map<InternDto>(model)).Returns(dto);

            var service = new InternService(internRepositoryStub.Object, mapper.Object, print.Object);

            var intern = await service.GetInternByIdAsync(id);
            Assert.NotNull(intern);
        }

        [Fact]
        public async Task GetInternByIdAsync_WithInvalidData_ReturnsNull()
        {
            var id = 200;

            internRepositoryStub.Setup(repo => repo.InternExistsAsync(id).Result).Returns(false);

            var service = new InternService(internRepositoryStub.Object, mapper.Object, print.Object);
            var intern = await service.GetInternByIdAsync(id);
            Assert.Null(intern);
        }

        [Fact]
        public async Task GetInternsAsync_ReturnDtoList()
        {
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

            internRepositoryStub.Setup(repo => repo.GetInternsAsync().Result).Returns(interns);
            mapper.Setup(map => map.Map<IEnumerable<InternListItemDto>>(interns)).Returns(dtos);

            var service = new InternService(internRepositoryStub.Object, mapper.Object, print.Object);
            var result = await service.GetInternsAsync();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }
        [Fact]
        public async Task PrintDecision_ProperData_ReturnDecisionDto()
        {
            var id = 99;
            var intern = new Intern { };
            var decisiondto = new DecisionDto { };

            internRepositoryStub.Setup(repo => repo.GetInternAsync(id).Result).Returns(intern);
            mapper.Setup(map => map.Map<DecisionDto>(intern)).Returns(decisiondto);


            var service = new InternService(internRepositoryStub.Object, mapper.Object, print.Object);
            var result = await service.PrintDecisionAsync(id);

            Assert.NotNull(result);

        }
        [Fact]
        public async Task PrintAttestation_ProperData_ReturnsAttestationDto()
        {
            var id = 99;
            var intern = new Intern { };
            var attestationDto = new AttestationDto { };

            internRepositoryStub.Setup(repo => repo.GetInternAsync(id).Result).Returns(intern);
            mapper.Setup(map => map.Map<AttestationDto>(intern)).Returns(attestationDto);


            var service = new InternService(internRepositoryStub.Object, mapper.Object, print.Object);
            var result = await service.PrintAttestationAsync(id);

            Assert.NotNull(result);
        }
        [Fact]
        public async Task PrintAnnulation_ProperData_ReturnsAnnulationDto()
        {
            var id = 99;
            var intern = new Intern { };
            var annulationDto = new AnnulationDto { };

            internRepositoryStub.Setup(repo => repo.GetInternAsync(id).Result).Returns(intern);
            mapper.Setup(map => map.Map<AnnulationDto>(intern)).Returns(annulationDto);


            var service = new InternService(internRepositoryStub.Object, mapper.Object, print.Object);
            var result = await service.PrintAnnulationAsync(id);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task SetDecisionAsync_ReturnsTrue()
        {
            var currentDate = DateTime.Today;
            var dto = new DecisionFormDto
            {
                InternId = 4,
                Code = "25444/2021",
                Date = currentDate
            };
            var model = new Decision
            {
                Id = dto.InternId,
                InternId = dto.InternId,
                Code = dto.Code,
                Date = dto.Date
            };

            mapper.Setup(map => map.Map<Decision>(dto)).Returns(model);
            internRepositoryStub.Setup(repo => repo.SetDecisionForIntern(dto.InternId, model).Result).Returns(true);
            var service = new InternService(internRepositoryStub.Object, mapper.Object, print.Object);

            var result = await service.SetDecisionAsync(dto);

            Assert.True(result);
        }
    }
}