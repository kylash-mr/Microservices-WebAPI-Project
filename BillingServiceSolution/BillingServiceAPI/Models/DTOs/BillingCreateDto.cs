namespace BillingService.DTOs
{
    public class BillingCreateDTO
    {
        public Guid PatientId { get; set; }
        public decimal Amount { get; set; }
        public DateTime BillingDate { get; set; }
        public string PaymentStatus { get; set; } = string.Empty;
        public string PaymentMethod { get; set; } = string.Empty;
        public List<BillingItemDTO> BillingItems { get; set; }
    }

    public class BillingReadDTO
    {
        public int BillingId { get; set; }
        public Guid PatientId { get; set; }
        public decimal Amount { get; set; }
        public DateTime BillingDate { get; set; }
        public string PaymentStatus { get; set; } = string.Empty;
        public string PaymentMethod { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<BillingItemDTO> BillingItems { get; set; }
    }

    public class BillingItemDTO
    {
        public string Description { get; set; } = string.Empty;
        public decimal Cost { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
    }
}
