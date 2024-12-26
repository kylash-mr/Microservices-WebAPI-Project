using System.ComponentModel.DataAnnotations;

namespace DoctorServiceAPI.Models
{

    public class Doctor
    {
        [Key]
        public int DoctorId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Specialty { get; set; } = string.Empty;
        public bool IsAvailable { get; set; } 
        public List<Schedule> Schedules { get; set; } = new List<Schedule>();
    }

}