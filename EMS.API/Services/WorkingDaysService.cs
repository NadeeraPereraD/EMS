using EMS.API.Interfaces;
using EMS.API.Models.DTOs;

namespace EMS.API.Services
{
    public class WorkingDaysService : IWorkingDaysService
    {
        private readonly IWorkingDaysRepository _repository;

        public WorkingDaysService(IWorkingDaysRepository repository)
        {
            _repository = repository;
        }

        public async Task<WorkingDaysResponseDto> GetWorkingDaysAsync(WorkingDaysRequestDto request)
        {
            var (days, error, success) = await _repository.GetWorkingDaysAsync(request.StartDate.Value, request.EndDate.Value);
            return new WorkingDaysResponseDto
            {
                days = days,
                ErrorMessage = error,
                SuccessMessage = success
            };
        }
    }
}
