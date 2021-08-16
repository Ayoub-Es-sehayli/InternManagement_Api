using System.Collections.Generic;
using System.Threading.Tasks;
using InternManagement.Api.Dtos;
using InternManagement.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InternManagement.Api.Controllers
{
  [ApiController]
  [Route("/api/{controller}")]
  public class InternsController : Controller
  {
    private readonly IInternService _service;

    public InternsController(IInternService service)
    {
      this._service = service;
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<InternDto>> GetIntern(int Id)
    {
      var intern = await _service.GetInternByIdAsync(Id);
      if (intern == null)
      {
        return NotFound();
      }
      return intern;
    }

    [HttpPost]
    public async Task<ActionResult<InternDto>> AddIntern(InternDto intern)
    {
      var dto = await _service.AddInternAsync(intern);

      return CreatedAtAction(nameof(GetIntern), new { Id = dto.Id }, dto);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<InternListItemDto>>> GetInterns()
    {
      var dtos = await _service.GetInternsAsync();

      return Ok(dtos);
    }

    [HttpPost]
    [Route("decision/{id}")]
    public async Task<ActionResult> SetDecision(DecisionFormDto dto)
    {
      if (!ModelState.IsValid)
        return BadRequest();

      var result = await _service.SetDecisionAsync(dto);

      if (result)
      {
        return Ok();
      }
      return BadRequest();
    }

    [HttpPost]
    [Route("attestation/{id}")]
    public async Task<ActionResult> SetAttestation(AttestationFormDto dto)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest();
      }

      var result = await _service.SetAttestationAsync(dto);

      if (result)
      { return Ok(); }

      return BadRequest();
    }
  }
}