using System.Threading.Tasks;
using InternManagement.Api.Dtos;
using InternManagement.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace InternManagement.Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserService _service;
        public UsersController(IUserService service)
        {
            this._service = service;
        }
        [HttpPost]
        public async Task<ActionResult<UserDto>> AddUser(UserDto model)
        {
            var result = await _service.AddUserAsync(model);
            if (result == null)
                return BadRequest(new { message = "vous n'avez pas saisi toutes les informations" });

            return Ok(result);
        }
        [HttpGet]
        public async Task<ActionResult<UserListDto>> GetUsers()
        {
            var result = await _service.GetUsersAsync();
            if (result == null)
            {
                return BadRequest(new { message = "La liste est vide !" });
            }
            return Ok(result);
        }
    }
}