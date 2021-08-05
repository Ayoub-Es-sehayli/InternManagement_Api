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
            this._config = config.GetSection("JwtKey").Get<JwtConfig>();
        }
        public async Task<UserDto> AddUserAsync(UserDto dto)
        {
            var user = await _repository.AddUserAsync(_mapper.Map<User>(dto));
            return dto;
        }
        public async Task<User> DeleteUserAsync(int id)
        {
            var user = await _repository.GetUserByIdAsync(id);
            return user;
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
        public Task<User> GetById(int id)
        {
            var user = _repository.FirstOrDefaultAsync(id);
            return user;
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
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}