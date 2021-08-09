using System.Collections.Generic;
using System.Threading.Tasks;
using InternManagement.Api.Dtos;

namespace InternManagement.Api.Services
{
  public interface IPunchInService
  {
    Task<IEnumerable<AttendanceDto>> GetAttendanceList();
    Task FlagInternEnterAsync(PunchInDto dto);
    Task FlagInternExitAsync(PunchInDto dto);
  }
}