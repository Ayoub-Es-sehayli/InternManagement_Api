using System.Collections.Generic;
using System.Threading.Tasks;
using InternManagement.Api.Models;

namespace InternManagement.Api.Repository
{
  public interface IPunchInRepository
  {
    Task<IEnumerable<Intern>> GetAttendanceList();
    Task FlagInternEnterAsync(Attendance model);
    Task FlagInternExitAsync(Attendance model);
  }
}