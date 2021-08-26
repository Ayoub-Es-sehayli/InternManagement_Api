using System.Collections.Generic;
using System.Threading.Tasks;
using InternManagement.Api.Dtos;
using InternManagement.Api.Models;


namespace InternManagement.Api.Services
{
  public interface IUserService
  {
    Task<UserDto> AddUserAsync(UserAddDto dto);
    Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest model);
    Task<IEnumerable<UserListDto>> GetUsersAsync();
    Task<UserDto> GetByIdAsync(int id);
    Task<UserDto> DeleteUserAsync(int id);
    Task<UserDto> EditUserAsync(int id, UserDto model);
  }
}