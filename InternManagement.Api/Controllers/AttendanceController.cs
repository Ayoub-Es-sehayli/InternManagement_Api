using System.Collections.Generic;
using System.Threading.Tasks;
using InternManagement.Api.Dtos;
using InternManagement.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace InternManagement.Api.Controllers
{
  [ApiController]
  [Route("/api/{controller}")]
  public class AttendanceController : Controller
  {
    private readonly IPunchInService _service;

    public AttendanceController(IPunchInService service)
    {
      this._service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AttendanceDto>>> GetAttendanceList()
    {
      var dtos = await _service.GetAttendanceList();

      if (dtos == null)
      {
        return BadRequest();
      }
      return Ok(dtos);
    }

    [HttpPut]
    public async Task<ActionResult> FlagInternEntered(PunchInDto dto)
    {
      await _service.FlagInternEnterAsync(dto);

      return Ok();
    }

    [HttpPost]
    public async Task<ActionResult> FlagInternExited(PunchInDto dto)
    {
      await _service.FlagInternExitAsync(dto);

      return Ok();
    }
  }
}