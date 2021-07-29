using System.Threading.Tasks;
using InternManagement.Api.Dtos;

namespace InternManagement.Api.Services
{
    public interface IUserService
    {
        Task<UserDto> AddUserAsync(UserDto user);
    }
}