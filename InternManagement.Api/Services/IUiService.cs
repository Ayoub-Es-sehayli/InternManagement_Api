using System.Collections.Generic;
using System.Threading.Tasks;
using InternManagement.Api.Dtos;

namespace InternManagement.Api.Services
{
  public interface IUiService
  {
    Task<IEnumerable<DepartmentDto>> GetDepartmentDtosAsync();
  }
}