using System.Collections.Generic;
using System.Threading.Tasks;
using InternManagement.Api.Dtos;

namespace InternManagement.Api.Services
{
  public interface IPreferencesService
  {
    Task<PreferencesDto> EditPreferencesAync(PreferencesDto dto);
    Task<PreferencesDto> GetPreferencesAsync();
  }
}
