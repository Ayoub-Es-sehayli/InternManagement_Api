using System.Threading.Tasks;
using InternManagement.Api.Dtos;
using InternManagement.Api.Enums;
using InternManagement.Api.Helpers;
using InternManagement.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace InternManagement.Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class PrintController : Controller
    {
        private readonly IPrintHelper _print;
        private readonly IInternService _service;
        public PrintController(IPrintHelper printHelper, IInternService service)
        {
            this._print = printHelper;
            this._service = service;
        }
        [HttpGet]
        [Route("decision/{id}")]
        public async Task<ActionResult<DecisionDto>> printDecision(int id)
        {
            var result = await _service.PrintDecisionAsync(id);
            if (result == null)
            {
                return BadRequest(new { message = "nothing to print" });
            }
            return Ok(result);
        }
        [HttpGet]
        [Route("annulation/{gender}")]
        public ActionResult<string> printCancel(eGender gender)
        {
            var result = _print.PrintCancel(gender);
            if (result == null)
            {
                return BadRequest(new { message = "nothing to print" });
            }
            return Ok(new { template = result });
        }
        [HttpGet]
        [Route("attestation/{id}")]
        public async Task<ActionResult<AttestationDto>> printCertificate(int id)
        {
            var result = await _service.PrintAttestationAsync(id);
            if (result == null)
            {
                return BadRequest(new { message = "nothing to print" });
            }
            return Ok(result);
        }
    }
}