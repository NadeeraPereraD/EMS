namespace EMS.API.Models.DTOs
{
    public class EmployeesResponseDto
    {
        public EmployeesDto? employeesDto {  get; set; }
        public string? ErrorMessage { get; set; }
        public string? SuccessMessage { get; set; }
    }
}
