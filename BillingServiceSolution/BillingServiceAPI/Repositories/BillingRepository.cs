using BillingServiceAPI.DbContexts;
using BillingServiceAPI.Interfaces;
using BillingServiceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BillingService.Repositories
{
    public class BillingRepository : IBillingRepository
    {
        private readonly BillingDbContext _context;

        public BillingRepository(BillingDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Billing>> GetAllBillingsAsync()
        {
            return await _context.Billings.Include(b => b.BillingItems).ToListAsync();
        }

        public async Task<Billing> GetBillingByIdAsync(int billingId)
        {
            if (billingId <= 0 )
                throw new ArgumentException("Invalid Billing ID.");

            return await _context.Billings.Include(b => b.BillingItems).FirstOrDefaultAsync(b => b.BillingId == billingId);
        }

        public async Task<IEnumerable<Billing>> GetBillingsByPatientIdAsync(Guid patientId)
        {
            if (patientId == Guid.Empty)
                throw new ArgumentException("Invalid Patient ID.");

            return await _context.Billings.Include(b => b.BillingItems)
                                           .Where(b => b.PatientId == patientId)
                                           .ToListAsync();
        }

        public async Task AddBillingAsync(Billing billing)
        {
            if (billing == null)
                throw new ArgumentNullException(nameof(billing), "Billing cannot be null.");
            if (billing.Amount <= 0)
                throw new ArgumentException("Billing amount must be greater than zero.");

            await _context.Billings.AddAsync(billing);
        }

        public async Task UpdateBillingAsync(Billing billing)
        {
            if (billing == null)
                throw new ArgumentNullException(nameof(billing), "Billing cannot be null.");
            if (billing.BillingId <= 0)
                throw new ArgumentException("Invalid Billing ID.");

            _context.Billings.Update(billing);
        }

        public async Task DeleteBillingAsync(int billingId)
        {
            if (billingId <= 0)
                throw new ArgumentException("Invalid Billing ID.");

            var billing = await GetBillingByIdAsync(billingId);
            if (billing != null)
            {
                _context.Billings.Remove(billing);
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }

    public class BillingItemRepository : IBillingItemRepository
    {
        private readonly BillingDbContext _context;

        public BillingItemRepository(BillingDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BillingItem>> GetBillingItemsByBillingIdAsync(int billingId)
        {
            if (billingId <= 0)
                throw new ArgumentException("Invalid Billing ID.");

            return await _context.BillingItems.Where(bi => bi.BillingId == billingId).ToListAsync();
        }

        public async Task<BillingItem> GetBillingItemByIdAsync(int billingItemId)
        {
            if (billingItemId <= 0)
                throw new ArgumentException("Invalid Billing Item ID.");

            return await _context.BillingItems.FirstOrDefaultAsync(bi => bi.BillingItemId == billingItemId);
        }

        public async Task AddBillingItemAsync(BillingItem billingItem)
        {
            if (billingItem == null)
                throw new ArgumentNullException(nameof(billingItem), "BillingItem cannot be null.");
            if (billingItem.Cost <= 0)
                throw new ArgumentException("Item cost must be greater than zero.");
            if (billingItem.Quantity <= 0)
                throw new ArgumentException("Item quantity must be greater than zero.");

            await _context.BillingItems.AddAsync(billingItem);
        }

        public async Task UpdateBillingItemAsync(BillingItem billingItem)
        {
            if (billingItem == null)
                throw new ArgumentNullException(nameof(billingItem), "BillingItem cannot be null.");
            if (billingItem.BillingItemId <= 0)
                throw new ArgumentException("Invalid Billing Item ID.");

            _context.BillingItems.Update(billingItem);
        }

        public async Task DeleteBillingItemAsync(int billingItemId)
        {
            if (billingItemId <= 0)
                throw new ArgumentException("Invalid Billing Item ID.");

            var billingItem = await GetBillingItemByIdAsync(billingItemId);
            if (billingItem != null)
            {
                _context.BillingItems.Remove(billingItem);
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
