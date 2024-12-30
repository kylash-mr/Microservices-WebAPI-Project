using BillingService.DTOs;

namespace BillingServiceAPI.Interfaces
{
    public interface IBillingService
    {
        Task<IEnumerable<BillingReadDTO>> GetAllBillingsAsync();
        Task<BillingReadDTO> GetBillingByIdAsync(int billingId);
        Task AddBillingAsync(BillingCreateDTO billingCreateDTO);
        Task UpdateBillingAsync(int billingId, BillingCreateDTO billingUpdateDTO);
        Task DeleteBillingAsync(int billingId);
    }
}
