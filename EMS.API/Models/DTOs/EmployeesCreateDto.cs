namespace EMS.API.Models.DTOs
{
    public class EmployeesCreateDto
    {
        public string name { get; set; } = null!;
        public string email { get; set; } = null!;
        public string jobPosition { get; set; } = null!;
    }
}
