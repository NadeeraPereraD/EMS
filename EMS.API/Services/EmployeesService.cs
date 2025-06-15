using AutoMapper;
using EMS.API.Interfaces;
using EMS.API.Models.DTOs;
using EMS.API.Models.Entities;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace EMS.API.Services
{
    public class EmployeesService : IEmployeesService
    {
        private readonly IEmployeesRepository _repository;
        private readonly IMapper _mapper;

        public EmployeesService(IEmployeesRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<(bool IsSuccess, string? ErrorMessage, string? SuccessMessage)> CreateAsync(EmployeesCreateDto dto)
            => await _repository.CreateAsync(dto);
        public Task<(IEnumerable<Employee> employees, string? ErrorMessage, string? SuccessMessage)> GetAllEmployeesAsync()
            => _repository.GetAllAsync();
        public async Task<EmployeesResponseDto> GetEmployeeAsync(EmployeesRequestDto request)
        {
            var (entity, error, success) = await _repository.GetByKeyAsync(request.Id);
            var dto = entity is not null ? _mapper.Map<EmployeesDto>(entity) : null;
            return new EmployeesResponseDto
            {
                employeesDto = dto,
                ErrorMessage = error,
                SuccessMessage = success
            };
        }
        public async Task<(bool IsSuccess, string? ErrorMessage, string? SuccessMessage)> UpdateAsyncByID(EmployeesUpdateDto dto)
            => await _repository.UpdateByKeyAsync(dto);
        public async Task<(bool IsSuccess, string? ErrorMessage, string? SuccessMessage)> SoftDeleteByID(EmployeesSoftDeleteDto dto)
            => await _repository.SoftDeleteByName(dto);
        public Task<(IEnumerable<Employee> employees, string? ErrorMessage, string? SuccessMessage)> GetAllInactiveEmployeesAsync()
            => _repository.GetAllInactiveAsync();
    }
}
