using System.Collections.Generic;
using System.Threading.Tasks;
using InternManagement.Api.Dtos;
using InternManagement.Api.Helpers;
using InternManagement.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternManagement.Api.Controllers
{
  [ApiController]
  [Route("/api/{controller}")]
  [Authorize(AuthorizationPolicies.Admin)]
  public class UiController : Controller
  {
    private readonly IUiService _service;

    public UiController(IUiService service)
    {
      this._service = service;
    }
    [HttpGet]
    [Route("divisions")]
    public async Task<ActionResult<IEnumerable<DepartmentDto>>> GetDepartments()
    {
      var dtos = await _service.GetDepartmentDtosAsync();

      if (dtos == null)
      {
        return BadRequest();
      }
      return Ok(dtos);
    }

  }
}