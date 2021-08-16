using System.Threading.Tasks;
using AutoMapper;
using InternManagement.Api.Dtos;
using InternManagement.Api.Models;
using InternManagement.Api.Repository;

namespace InternManagement.Api.Services
{
  public class PreferencesService : IPreferencesService
  {
    private readonly IPreferencesRepository _repository;
    private readonly IMapper _mapper;

    public PreferencesService(IPreferencesRepository repository, IMapper mapper)
    {
      this._repository = repository;
      this._mapper = mapper;
    }
    public async Task<PreferencesDto> EditPreferencesAync(PreferencesDto dto)
    {
      var result = await _repository.EditPreferencesAsync(_mapper.Map<Preferences>(dto));
      return _mapper.Map<PreferencesDto>(result);
    }

    public async Task<PreferencesDto> GetPreferencesAsync()
    {
      var result = await _repository.GetPreferencesAsync();

      return _mapper.Map<PreferencesDto>(result);
    }
  }

}