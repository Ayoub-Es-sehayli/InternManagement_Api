using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using InternManagement.Api.Dtos;
using InternManagement.Api.Models;
using InternManagement.Api.Repository;

namespace InternManagement.Api.Services
{
  public class PunchInService : IPunchInService
  {
    private IPunchInRepository _repository;
    private IMapper _mapper;

    public PunchInService(IPunchInRepository repository, IMapper mapper)
    {
      this._repository = repository;
      this._mapper = mapper;
    }

    public async Task FlagInternEnterAsync(PunchInDto dto)
    {
      var model = _mapper.Map<Attendance>(dto);
      await _repository.FlagInternEnterAsync(model);
    }

    public async Task FlagInternExitAsync(PunchInDto dto)
    {
      var model = _mapper.Map<Attendance>(dto);
      await _repository.FlagInternExitAsync(model);
    }

    public async Task<IEnumerable<AttendanceDto>> GetAttendanceList()
    {
      var models = await _repository.GetAttendanceList();

      return _mapper.Map<IEnumerable<AttendanceDto>>(models);
    }
  }
}