using BillingService.DTOs;

namespace BillingServiceAPI.Interfaces
{
    public interface IBillingItemService
    {
        Task<IEnumerable<BillingItemDTO>> GetBillingItemsByBillingIdAsync(int billingId);
        Task AddBillingItemAsync(BillingItemDTO billingItemDTO);
        Task UpdateBillingItemAsync(int billingItemId, BillingItemDTO billingItemDTO);
        Task DeleteBillingItemAsync(int billingItemId);
    }
}
