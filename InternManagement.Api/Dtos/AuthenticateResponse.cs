using InternManagement.Api.Enums;
using InternManagement.Api.Models;

namespace InternManagement.Api.Dtos
{
  public class AuthenticateResponse
  {
    public AuthenticateResponse(User user, string token)
    {
      Role = user.Role;
      this.Token = token;
    }

    public eUserRole Role { get; set; }
    public string Token { get; set; }
  }
}