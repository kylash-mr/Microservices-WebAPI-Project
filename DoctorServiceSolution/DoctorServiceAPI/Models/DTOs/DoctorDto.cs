namespace DoctorServiceAPI.Models.DTOs
{
    public class DoctorDto
    {
        public string Name { get; set; } = string.Empty;
        public string Specialty { get; set; } = string.Empty;
        public bool IsAvailable { get; set; }
    }
}
