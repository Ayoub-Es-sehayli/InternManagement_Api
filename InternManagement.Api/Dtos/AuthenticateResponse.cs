using InternManagement.Api.Enums;

namespace InternManagement.Api.Models
{
    public class AuthenticateResponse
    {
        public AuthenticateResponse(User user, string token)
        {
            Id = user.Id;
            Username = user.Email;
            Role = user.Role;
            this.Token = token;
        }

        public int Id { get; set; }
        public eUserRole Role { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
    }
}