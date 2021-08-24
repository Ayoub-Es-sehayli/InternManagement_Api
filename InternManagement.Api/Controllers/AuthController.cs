using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using InternManagement.Api.Dtos;
using InternManagement.Api.Enums;
using InternManagement.Api.Models;
using InternManagement.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternManagement.Api.Controllers
{
  [ApiController]
  [Route("/api/[controller]")]
  public class AuthController : Controller
  {
    private readonly IUserService _service;

    public AuthController(IUserService service)
    {
      this._service = service;
    }
    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<AuthenticateResponse>> Authenticate(AuthenticateRequest model)
    {
      var response = await _service.AuthenticateAsync(model);

      if (response == null)
        return Unauthorized(new { message = "Email ou mot de passe est incorrecte" });

      return Json(response);
    }

    [HttpGet]
    [Authorize]
    public ActionResult<AuthRoleDto> GetLoggedInInfo()
    {
      var authHeader = Request.Headers["Authorization"].ToString().Split(" ")[1];

      var token = new JwtSecurityTokenHandler().ReadJwtToken(authHeader);

      var claim = token.Claims.First(claim => claim.Type == "role");

      var role = (eUserRole)Enum.Parse(typeof(eUserRole), claim.Value);

      return Ok(new AuthRoleDto { Role = role });
    }
  }
}