using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using InternManagement.Api.Dtos;
using InternManagement.Api.Repository;

namespace InternManagement.Api.Services
{
  public class DashboardService : IDashboardService
  {
    private readonly IDashboardRepository _repository;
    private readonly IMapper _mapper;

    public DashboardService(IDashboardRepository repository, IMapper mapper)
    {
      this._repository = repository;
      this._mapper = mapper;
    }

    public async Task<IEnumerable<LatestInternDto>> GetLatestInternsAsync()
    {
      var interns = await _repository.GetLatestInternsAsync();
      var dtos = _mapper.Map<IEnumerable<LatestInternDto>>(interns);

      return dtos;
    }
    public async Task<IEnumerable<AlertInternDto>> GetAlertInternsAsync()
    {
      var interns = await _repository.GetAlertInternsAsync();
      var dtos = _mapper.Map<IEnumerable<AlertInternDto>>(interns);

      return dtos;
    }

    public async Task<IEnumerable<FinishingInternDto>> GetFinishingInternsAsync()
    {
      var interns = await _repository.GetFinishingInternsAsync();
      var dtos = _mapper.Map<IEnumerable<FinishingInternDto>>(interns);

      return dtos;
    }
  }
}