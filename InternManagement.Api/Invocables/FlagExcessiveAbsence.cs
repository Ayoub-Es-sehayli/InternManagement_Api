using System.Threading.Tasks;
using Coravel.Invocable;
using InternManagement.Api.Repository;

namespace InternManagement.Api.Invocables
{
  public class FlagExcessiveAbsence : IInvocable
  {
    private readonly IScheduledJobsRepository _repository;

    public FlagExcessiveAbsence(IScheduledJobsRepository repository)
    {
      this._repository = repository;
    }
    public async Task Invoke()
    {
      await _repository.FlagExcessiveAbsenceAsync();
    }
  }
}