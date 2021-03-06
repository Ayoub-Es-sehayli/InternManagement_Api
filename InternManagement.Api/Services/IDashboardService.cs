using System.Collections.Generic;
using System.Threading.Tasks;
using InternManagement.Api.Dtos;

namespace InternManagement.Api.Services
{
  public interface IDashboardService
  {
    Task<IEnumerable<LatestInternDto>> GetLatestInternsAsync();
    Task<IEnumerable<FinishingInternDto>> GetFinishingInternsAsync();
    Task<IEnumerable<AlertInternDto>> GetAlertInternsAsync();
    Task<GeneralStatsDto> GetStatsAsync();
  }
}