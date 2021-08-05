using InternManagement.Api.Enums;
using InternManagement.Api.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace InternManagement.Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class PrintController : Controller
    {
        private readonly PrintHelper _print;
        public PrintController()
        {
            this._print = new PrintHelper();
        }
        [HttpGet]
        [Route("decision/{gender}")]
        public ActionResult<string> printDecision(eGender gender)
        {
            var result = _print.PrintDecision(gender);
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
            return Ok(result);
        }
        [HttpGet]
        [Route("attestation/{gender}")]
        public ActionResult<string> printCertificate(eGender gender)
        {
            var result = _print.PrintCertificate(gender);
            if (result == null)
            {
                return BadRequest(new { message = "nothing to print" });
            }
            return Ok(result);
        }
    }
}