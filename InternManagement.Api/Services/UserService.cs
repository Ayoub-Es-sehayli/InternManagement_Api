using System.Threading.Tasks;
using AutoMapper;
using InternManagement.Api.Dtos;
using InternManagement.Api.Models;
using InternManagement.Api.Repository;

namespace InternManagement.Api.Services
{
    public class UserService : IUserService 
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        public async Task<UserDto> AddUserAsync(UserDto dto)
        {
            var user = await _repository.AddUserAsync(_mapper.Map<User>(dto));
            dto.EmailAddress = user.Email;
            return dto;
        }
    }
}