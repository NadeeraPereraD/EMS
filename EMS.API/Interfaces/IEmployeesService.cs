using EMS.API.Models.DTOs;
using EMS.API.Models.Entities;

namespace EMS.API.Interfaces
{
    public interface IEmployeesService
    {
        Task<(bool IsSuccess, string? ErrorMessage, string? SuccessMessage)> CreateAsync(EmployeesCreateDto dto);
        Task<(IEnumerable<Employee> employees, string? ErrorMessage, string? SuccessMessage)> GetAllEmployeesAsync();
        Task<EmployeesResponseDto> GetEmployeeAsync(EmployeesRequestDto request);
        Task<(bool IsSuccess, string? ErrorMessage, string? SuccessMessage)> UpdateAsyncByID(EmployeesUpdateDto dto);
        Task<(bool IsSuccess, string? ErrorMessage, string? SuccessMessage)> SoftDeleteByID(EmployeesSoftDeleteDto dto);
        Task<(IEnumerable<Employee> employees, string? ErrorMessage, string? SuccessMessage)> GetAllInactiveEmployeesAsync();
    }
}
