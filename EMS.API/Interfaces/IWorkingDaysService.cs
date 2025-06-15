using EMS.API.Models.DTOs;

namespace EMS.API.Interfaces
{
    public interface IWorkingDaysService
    {
        Task<WorkingDaysResponseDto> GetWorkingDaysAsync(WorkingDaysRequestDto request);
    }
}
