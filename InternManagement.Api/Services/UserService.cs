using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using InternManagement.Api.Dtos;
using InternManagement.Api.Models;
using InternManagement.Api.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace InternManagement.Api.Services
{
  public class UserService : IUserService
  {
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;
    private readonly JwtConfig _config;

    public UserService(IUserRepository repository, IMapper mapper, IConfiguration config)
    {
      this._repository = repository;
      this._mapper = mapper;
      this._config = config.GetSection("Jwt").Get<JwtConfig>();
    }
    public async Task<UserDto> AddUserAsync(UserAddDto dto)
    {
      var user = await _repository.AddUserAsync(_mapper.Map<User>(dto));
      return _mapper.Map<UserDto>(user);
    }
    public async Task<UserDto> DeleteUserAsync(int id)
    {
      var user = await _repository.DeleteUserAsync(id);
      return _mapper.Map<UserDto>(user);
    }
    public async Task<UserDto> EditUserAsync(int id, UserDto model)
    {
      var user = await _repository.EditUserAsync(id, _mapper.Map<User>(model));
      return _mapper.Map<UserDto>(user);
    }
    public async Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest model)
    {
      var user = await _repository.UserExistsAsync(model.Username, model.Password);
      if (user != null)
      {
        var authResponse = new AuthenticateResponse(user, generateJwtToken(user));
        return authResponse;
      }
      return null;
    }
    public async Task<IEnumerable<UserListDto>> GetUsersAsync()
    {
      var users = await _repository.GetUsersAsync();
      var dtos = _mapper.Map<IEnumerable<UserListDto>>(users);
      return dtos;
    }
    private string generateJwtToken(User user)
    {
      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(_config.Salt);
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Audience = _config.Audience,
        Issuer = _config.Issuer,
        Subject = new ClaimsIdentity(new[] {
          new Claim("id", user.Id.ToString()),
          new Claim("role", Enum.GetName(user.Role))
         }),
        Expires = DateTime.UtcNow.AddDays(1),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };
      var token = tokenHandler.CreateToken(tokenDescriptor);
      return tokenHandler.WriteToken(token);
    }
  }
}