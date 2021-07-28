using System.Threading.Tasks;
using InternManagement.Api.Dtos;

namespace InternManagement.Api.Services
{
  public interface IInternService
  {
    Task<InternDto> AddInternAsync(InternDto dto);
    Task<InternDto> GetInternByIdAsync(int id);
  }
}