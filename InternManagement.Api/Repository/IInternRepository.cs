using System.Collections.Generic;
using System.Threading.Tasks;
using InternManagement.Api.Models;

namespace InternManagement.Api.Repository
{
  public interface IInternRepository
  {
    Task<Intern> AddInternAsync(Intern model);
    Task<bool> InternExistsAsync(int id);
    Task<Intern> GetInternAsync(int id);
    Task<Intern> GetInternWithDecision(int id);
    Task<Intern> GetInternWithDepartment(int id);
    Task<Intern> GetInternWithDepartmentAndLocation(int id);
    Task<IEnumerable<Intern>> GetInternsAsync();
    Task<bool> SetDecisionForIntern(int id, Decision decision);
    Task<bool> SetAttestationForIntern(int id, Attestation attestation);
    Task<bool> SetCancellationForIntern(int id, Cancellation cancellation);
  }
}