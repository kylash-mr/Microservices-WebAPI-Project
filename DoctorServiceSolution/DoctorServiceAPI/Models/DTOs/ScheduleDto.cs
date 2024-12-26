namespace DoctorServiceAPI.Models.DTOs
{
    public class ScheduleDto
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string DayOfWeek { get; set; } = string.Empty;
        public bool IsBooked { get; set; }
    }
}
