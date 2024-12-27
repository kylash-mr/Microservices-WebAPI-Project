using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PatientRecordServiceAPI.Models
{
    public class MedicalRecords
    {
        [Key]
        public int RecordId { get; set; }

        [Required]
        public int PatientId { get; set; }

        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }

        [Required]
        public string Diagnosis { get; set; } = string.Empty;

        public string Treatment { get; set; } = string.Empty;

        public DateTime RecordDate { get; set; } = DateTime.UtcNow;

    }
}
