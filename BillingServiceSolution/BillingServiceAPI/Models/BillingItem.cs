using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BillingServiceAPI.Models
{
    public class BillingItem
    {
        [Key]
        public int BillingItemId { get; set; }

        [Required]
        public int BillingId { get; set; }

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public decimal Cost { get; set; }

        public int Quantity { get; set; }

        public decimal Total => Cost * Quantity;

        [ForeignKey("BillingId")]
        public Billing Billing { get; set; }
    }
}
