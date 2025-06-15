
using EMS.API.Models.DTOs;
using EMS.API.Models.Entities;

namespace EMS.API.Interfaces
{
    public interface IEmployeesRepository
    {
        Task<(bool IsSuccess, string? ErrorMessage, string? SuccessMessage)> CreateAsync(EmployeesCreateDto dto);
        Task<(IEnumerable<Employee> employees, string? ErrorMessage, string? SuccessMessage)> GetAllAsync();
        Task<(Employee? Entity, string? ErrorMessage, string? SuccessMessage)> GetByKeyAsync(int Id);
        Task<(bool IsSuccess, string? ErrorMessage, string? SuccessMessage)> UpdateByKeyAsync(EmployeesUpdateDto dto);
        Task<(bool IsSuccess, string? ErrorMessage, string? SuccessMessage)>SoftDeleteByName(EmployeesSoftDeleteDto dto);
        Task<(IEnumerable<Employee> employees, string? ErrorMessage, string? SuccessMessage)> GetAllInactiveAsync();
    }
}
