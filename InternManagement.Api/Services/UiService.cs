using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using InternManagement.Api.Dtos;
using InternManagement.Api.Repository;

namespace InternManagement.Api.Services
{
  public class UiService : IUiService
  {
    private readonly IUiRepository _repository;
    private readonly IMapper _mapper;

    public UiService(IUiRepository repository, IMapper mapper)
    {
      this._repository = repository;
      this._mapper = mapper;
    }
    public async Task<IEnumerable<DepartmentDto>> GetDepartmentDtosAsync()
    {
      var models = await _repository.GetDepartmentsAsync();
      var dtos = _mapper.Map<IEnumerable<DepartmentDto>>(models);

      return dtos;
    }
  }
}