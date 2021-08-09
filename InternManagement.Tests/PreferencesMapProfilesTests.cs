using AutoMapper;
using InternManagement.Api.Dtos;
using InternManagement.Api.Models;
using InternManagement.Api.Profiles;
using Xunit;

namespace InternManagement.Tests
{
    public class PreferencesMapProfilesTests
    {
        [Fact]
        public void MapPreferences_ReturnDtp()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<PreferenceProfile>();
            });
            var mapper = config.CreateMapper();

            var model = new Preferences
            {
                nInterns = 0,
                nAttendanceDays = 0
            };

            var dto = new PreferencesDto
            {
                nInterns = model.nInterns,
                nAttendanceDays = model.nAttendanceDays
            };
            var result = mapper.Map<Preferences>(dto);

            Assert.Equal(model.nInterns, result.nInterns);
            Assert.Equal(model.nAttendanceDays, result.nAttendanceDays);
        }
    }
}