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
  public class DashboardController : Controller
  {
    private readonly IDashboardService _service;

    public DashboardController(IDashboardService service)
    {
      this._service = service;
    }

    [HttpGet]
    [Route("latest")]
    public async Task<ActionResult<LatestInternDto>> GetLatestInterns()
    {
      var dtos = await _service.GetLatestInternsAsync();

      return Ok(dtos);
    }
    [HttpGet]
    [Route("finishing")]
    public async Task<ActionResult<FinishingInternDto>> GetFinishingInterns()
    {
      var dtos = await _service.GetFinishingInternsAsync();

      return Ok(dtos);
    }
    [HttpGet]
    [Route("alerts")]
    public async Task<ActionResult<AlertInternDto>> GetAlarmingInterns()
    {
      var dtos = await _service.GetAlertInternsAsync();

      return Ok(dtos);
    }
    [HttpGet]
    [Route("stats")]
    public async Task<ActionResult<GeneralStatsDto>> GetStats()
    {
      var stats = await _service.GetStatsAsync();

      return Ok(stats);
    }
  }
}