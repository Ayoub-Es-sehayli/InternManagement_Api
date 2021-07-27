using System.Threading.Tasks;
using InternManagement.Api.Models;

namespace InternManagement.Api.Repository {
  public interface IInternRepository {
    Task<Intern> AddInternAsync(Intern model);
  }
}