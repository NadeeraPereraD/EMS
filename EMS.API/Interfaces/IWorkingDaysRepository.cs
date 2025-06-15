using EMS.API.Models.Entities;

namespace EMS.API.Interfaces
{
    public interface IWorkingDaysRepository
    {
        Task<(int days, string? ErrorMessage, string? SuccessMessage)> GetWorkingDaysAsync(DateTime startDate, DateTime endDate);
    }
}
