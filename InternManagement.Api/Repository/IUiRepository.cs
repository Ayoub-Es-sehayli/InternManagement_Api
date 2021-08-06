using System.Collections.Generic;
using System.Threading.Tasks;
using InternManagement.Api.Models;

namespace InternManagement.Api.Repository
{
  public interface IUiRepository
  {
    Task<IEnumerable<Department>> GetDepartmentsAsync();
  }
}