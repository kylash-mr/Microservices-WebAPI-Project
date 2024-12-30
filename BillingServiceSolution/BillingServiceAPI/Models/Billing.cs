using System.ComponentModel.DataAnnotations;

namespace BillingServiceAPI.Models
{
    public class Billing
    {
        [Key]
        public int BillingId { get; set; }

        [Required]
        public Guid PatientId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime BillingDate { get; set; }

        public string PaymentStatus { get; set; }

        public string PaymentMethod { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<BillingItem> BillingItems { get; set; }
    }
}
