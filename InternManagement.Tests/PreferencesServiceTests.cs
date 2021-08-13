using System.Threading.Tasks;
using AutoMapper;
using InternManagement.Api.Dtos;
using InternManagement.Api.Models;
using InternManagement.Api.Repository;
using InternManagement.Api.Services;
using Moq;
using Xunit;

namespace InternManagement.Tests
{
    public class PreferencesServiceTests
    {
        private readonly Mock<IPreferencesRepository> preferencesRepositoryStub = new();
        private readonly Mock<IMapper> mapper = new();
        [Fact]
        public async Task EditPreferencesAsync_ProperData_ReturnsDto()
        {
            var model = new Preferences
            {
                nInterns = 0,
                nAttendanceDays = 0
            };

            var preferences = new PreferencesDto
            {
                nInterns = model.nInterns,
                nAttendanceDays = model.nAttendanceDays
            };
            preferencesRepositoryStub.Setup(repo => repo.EditPreferencesAsync(model).Result).Returns(model);
            mapper.Setup(map => map.Map<Preferences>(preferences)).Returns(model);
            mapper.Setup(map => map.Map<PreferencesDto>(model)).Returns(preferences);

            var service = new PreferencesService(preferencesRepositoryStub.Object, mapper.Object);
            var result = await service.EditPreferencesAync(preferences);

            Assert.NotNull(result);
        }
    }
}
