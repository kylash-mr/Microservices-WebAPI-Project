using BillingServiceAPI.Interfaces;
using BillingServiceAPI.Models;
using BillingService.DTOs;

namespace BillingServiceAPI.Services
{
    public class BillingItemService : IBillingItemService
    {
        private readonly IBillingItemRepository _billingItemRepository;

        public BillingItemService(IBillingItemRepository billingItemRepository)
        {
            _billingItemRepository = billingItemRepository;
        }

        public async Task<IEnumerable<BillingItemDTO>> GetBillingItemsByBillingIdAsync(int billingId)
        {
            var billingItems = await _billingItemRepository.GetBillingItemsByBillingIdAsync(billingId);
            var billingItemDTOs = billingItems.Select(bi => new BillingItemDTO
            {
                Description = bi.Description,
                Cost = bi.Cost,
                Quantity = bi.Quantity,
                Total = bi.Total
            });

            return billingItemDTOs;
        }

        public async Task AddBillingItemAsync(BillingItemDTO billingItemDTO)
        {
            var billingItem = new BillingItem
            {
                Description = billingItemDTO.Description,
                Cost = billingItemDTO.Cost,
                Quantity = billingItemDTO.Quantity
            };

            await _billingItemRepository.AddBillingItemAsync(billingItem);
            await _billingItemRepository.SaveAsync();
        }

        public async Task UpdateBillingItemAsync(int billingItemId, BillingItemDTO billingItemDTO)
        {
            var billingItem = await _billingItemRepository.GetBillingItemByIdAsync(billingItemId);
            if (billingItem == null) throw new Exception("Billing item not found.");

            billingItem.Description = billingItemDTO.Description;
            billingItem.Cost = billingItemDTO.Cost;
            billingItem.Quantity = billingItemDTO.Quantity;

            await _billingItemRepository.UpdateBillingItemAsync(billingItem);
            await _billingItemRepository.SaveAsync();
        }

        public async Task DeleteBillingItemAsync(int billingItemId)
        {
            var billingItem = await _billingItemRepository.GetBillingItemByIdAsync(billingItemId);
            if (billingItem == null) throw new Exception("Billing item not found.");

            await _billingItemRepository.DeleteBillingItemAsync(billingItemId);
            await _billingItemRepository.SaveAsync();
        }
    }
}
