using Azure.Core;
using EMS.API.Interfaces;
using EMS.API.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeesService _employeesService;

        public EmployeesController(IEmployeesService employeesService)
        {
            _employeesService = employeesService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EmployeesCreateDto dto)
        {
            var result = await _employeesService.CreateAsync(dto);
            bool ok = result.IsSuccess;
            string? error = result.ErrorMessage;
            string? success = result.SuccessMessage;

            if (!ok)
            {
                return BadRequest(new { error });
            }
            return Ok(new {message = success});
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var (employees, error, success) = await _employeesService.GetAllEmployeesAsync();
            var list = employees;

            if (!string.IsNullOrEmpty(error) && list is not { } || list.Any() is false)
                return NotFound(new { Message = error });

            return Ok(new
            {
                Message = success,
                Data = employees
            });
        }

        [HttpPost("by-key")]
        public async Task<IActionResult> GetByKey([FromBody] EmployeesRequestDto request)
        {
            if (request == null || string.IsNullOrEmpty(request.name))
                return BadRequest(new { message = "Employee name is required in the request body." });

            var result = await _employeesService.GetEmployeeAsync(request);
            if (!string.IsNullOrEmpty(result.ErrorMessage))
                return NotFound(new { message = result.ErrorMessage });

            return Ok(new { data = result.employeesDto, message = result.SuccessMessage });
        }

        [HttpPut("by-key")]
        public async Task<IActionResult> UpdateByKey([FromBody] EmployeesCreateDto dto)
        {
            if (dto == null || string.IsNullOrEmpty(dto.name))
                return BadRequest(new { message = "Employee name is required in the request body." });
            var result = await _employeesService.UpdateAsyncByID(dto);
            bool ok = result.IsSuccess;
            string? error = result.ErrorMessage;
            string? success = result.SuccessMessage;

            if (!ok)
            {
                return BadRequest(new { error });
            }
            return Ok(new { message = success });
        }

        [HttpPut("soft-delete-by-key")]
        public async Task<IActionResult> SoftDeleteByCode([FromBody] EmployeesSoftDeleteDto dto)
        {
            if (dto == null || string.IsNullOrEmpty(dto.name))
                return BadRequest(new { message = "Employee name is required in the request body." });
            var result = await _employeesService.SoftDeleteByID(dto);
            bool ok = result.IsSuccess;
            string? error = result.ErrorMessage;
            string? success = result.SuccessMessage;

            if (!ok)
            {
                return BadRequest(new { error });
            }
            return Ok(new { message = success });
        }

        [HttpGet]
        [Route("inactive")]
        public async Task<IActionResult> GetAllInactive()
        {
            var (employees, error, success) = await _employeesService.GetAllInactiveEmployeesAsync();
            var list = employees;

            if (!string.IsNullOrEmpty(error) && list is not { } || list.Any() is false)
                return NotFound(new { Message = error });

            return Ok(new
            {
                Message = success,
                Data = employees
            });
        }
    }
}
