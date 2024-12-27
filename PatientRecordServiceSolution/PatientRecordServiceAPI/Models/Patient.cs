using System.ComponentModel.DataAnnotations;

namespace PatientRecordServiceAPI.Models
{
    public class Patient
    {
        [Key]
        public int PatientId { get; set; }

        [Required]
        [StringLength(100)]
        public string PatientName { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(15)]
        public string PhoneNumber { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }

        public string Gender { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<MedicalRecords>? MedicalRecords { get; set; }
    }

}
