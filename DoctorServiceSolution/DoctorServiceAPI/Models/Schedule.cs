using System.ComponentModel.DataAnnotations;

namespace DoctorServiceAPI.Models
{
    public class Schedule
    {
        [Key]
        public int ScheduleId { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public string DayOfWeek { get; set; } = string.Empty;

        public bool IsBooked { get; set; } = false;

        [Required]
        public int DoctorId { get; set; }
    }
}
