using EMS.API.Interfaces;
using EMS.API.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkingDaysController : ControllerBase
    {
        private readonly IWorkingDaysService _workingDaysService;

        public WorkingDaysController(IWorkingDaysService workingDaysService)
        {
            _workingDaysService = workingDaysService;
        }

        [HttpPost("by-key")]
        public async Task<IActionResult> GetByKey([FromBody] WorkingDaysRequestDto request)
        {
            if (request == null || request.StartDate == null || request.EndDate == null)
                return BadRequest(new { message = "Start date & End Date is required in the request body." });

            var result = await _workingDaysService.GetWorkingDaysAsync(request);
            if (!string.IsNullOrEmpty(result.ErrorMessage))
                return NotFound(new { message = result.ErrorMessage });

            return Ok(new { data = result.days, message = result.SuccessMessage });
        }
    }
}
