using BillingServiceAPI.Models;

namespace BillingServiceAPI.Interfaces
{
    public interface IBillingRepository
    {
        Task<IEnumerable<Billing>> GetAllBillingsAsync();
        Task<Billing> GetBillingByIdAsync(int billingId);
        Task<IEnumerable<Billing>> GetBillingsByPatientIdAsync(Guid patientId);
        Task AddBillingAsync(Billing billing);
        Task UpdateBillingAsync(Billing billing);
        Task DeleteBillingAsync(int billingId);
        Task SaveAsync();
    }
}
