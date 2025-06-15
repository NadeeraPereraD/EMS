namespace EMS.API.Models.DTOs
{
    public class WorkingDaysResponseDto
    {
        public int days {  get; set; }
        public string? ErrorMessage { get; set; }
        public string? SuccessMessage { get; set; }
    }
}
