using System.Threading.Tasks;
using AutoMapper;
using InternManagement.Api.Dtos;
using InternManagement.Api.Models;
using InternManagement.Api.Repository;

namespace InternManagement.Api.Services
{
  public class InternService : IInternService
  {
    private readonly IInternRepository _repository;
    private readonly IMapper _mapper;

    public InternService(IInternRepository repository, IMapper mapper)
    {
      this._repository = repository;
      this._mapper = mapper;
    }

    public async Task<InternDto> AddInternAsync(InternDto dto)
    {
      var intern = await _repository.AddInternAsync(_mapper.Map<Intern>(dto));
      dto.Id = intern.Id;
      return dto;
    }

    public async Task<InternDto> GetInternByIdAsync(int id)
    {
      if (await _repository.InternExistsAsync(id))
      {
        var intern = await _repository.GetInternAsync(id);
        return _mapper.Map<InternDto>(intern);
      }
      return null;
    }
  }
}