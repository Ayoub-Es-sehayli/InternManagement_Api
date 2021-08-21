using System.Threading.Tasks;
using Coravel.Invocable;
using InternManagement.Api.Repository;

namespace InternManagement.Api.Invocables
{
  public class AddInternAttendanceEntry : IInvocable
  {
    private readonly IScheduledJobsRepository _repository;

    public AddInternAttendanceEntry(IScheduledJobsRepository repository)
    {
      this._repository = repository;
    }
    public async Task Invoke()
    {
      await _repository.AddInternAttendanceEntryAsync();
    }
  }
}