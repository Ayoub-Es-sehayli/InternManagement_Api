using System;
using System.Threading.Tasks;
using Coravel.Invocable;
using InternManagement.Api.Repository;
using Microsoft.Extensions.Logging;

namespace InternManagement.Api.Invocables
{
  public class AddInternAttendanceEntry : IInvocable
  {
    private readonly IScheduledJobsRepository _repository;
    private readonly ILogger _logger;

    public AddInternAttendanceEntry(IScheduledJobsRepository repository, ILogger<AddInternAttendanceEntry> logger)
    {
      this._repository = repository;
      this._logger = logger;
    }
    public async Task Invoke()
    {
      await _repository.AddInternAttendanceEntryAsync();
      _logger.LogInformation($"Have Marked active Interns as Absent for {DateTime.Today.ToShortDateString()}");
    }
  }
}