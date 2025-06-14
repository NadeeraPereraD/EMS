namespace EMS.API.Models.DTOs
{
    public class EmployeesDto
    {
        public int Id { get; set; }
        public string name { get; set; } = null!;
        public string email { get; set; } = null!;
        public string jobPosition { get; set; } = null!;
        public bool isActive { get; set; }
    }
}
