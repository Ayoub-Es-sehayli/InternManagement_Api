using System.Threading.Tasks;
using InternManagement.Api.Dtos;
using InternManagement.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace InternManagement.Api.Controllers
{
  [ApiController]
  [Route("/api/[controller]")]
  public class PreferencesController : Controller
  {
    private readonly IPreferencesService _service;
    public PreferencesController(IPreferencesService service)
    {
      this._service = service;
    }
    [HttpGet]
    public async Task<ActionResult<PreferencesDto>> GetPreferencesAsync()
    {
      var result = await _service.GetPreferencesAsync();

      return Ok(result);
    }
    [HttpPut]
    public async Task<ActionResult<PreferencesDto>> EditPreferencesAsync(PreferencesDto preferences)
    {
      var result = await _service.EditPreferencesAync(preferences);

      return Ok(result);
    }

  }
}