using System.Threading.Tasks;
using InternManagement.Api.Models;
using InternManagement.Api.Services;
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
        public async Task<IActionResult> Authenticate(AuthenticateRequest model)
        {
            var response = await _service.AuthenticateAsync(model);

            if (response == null)
                return BadRequest(new { message = "Email or password is incorrect" });

            return Ok(response);
        }
    }
}