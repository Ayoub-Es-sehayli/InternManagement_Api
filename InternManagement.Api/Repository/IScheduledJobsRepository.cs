using System.Threading.Tasks;

namespace InternManagement.Api.Repository
{
  public interface IScheduledJobsRepository
  {
    Task<int> UpdateInternStateAsync();
    Task<int> AddInternAttendanceEntryAsync();
    Task<int> FlagExcessiveAbsenceAsync();
  }
}