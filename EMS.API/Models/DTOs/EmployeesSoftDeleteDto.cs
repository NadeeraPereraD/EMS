namespace EMS.API.Models.DTOs
{
    public class EmployeesSoftDeleteDto
    {
        public string name { get; set; } = null!;
        public bool? isActive {  get; set; }
    }
}
