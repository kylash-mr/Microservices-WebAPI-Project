using BillingServiceAPI.Models;

namespace BillingServiceAPI.Interfaces
{
    public interface IBillingItemRepository
    {
        Task<IEnumerable<BillingItem>> GetBillingItemsByBillingIdAsync(int billingId);
        Task<BillingItem> GetBillingItemByIdAsync(int billingItemId);
        Task AddBillingItemAsync(BillingItem billingItem);
        Task UpdateBillingItemAsync(BillingItem billingItem);
        Task DeleteBillingItemAsync(int billingItemId);
        Task SaveAsync();
    }
}
