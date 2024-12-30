using BillingServiceAPI.Interfaces;
using BillingServiceAPI.Models;
using BillingService.DTOs;

namespace BillingsServiceAPI.Services
{
    public class BillingsService : IBillingService
    {
        private readonly HttpClient _userManagementClient;
        private readonly HttpClient _appointmentClient;
        private readonly HttpClient _doctorClient;
        private readonly HttpClient _patientRecordClient;
        private readonly IBillingRepository _billingRepository;
        private readonly IBillingItemRepository _billingItemRepository;

        public BillingsService(
            HttpClient userManagementClient,
            HttpClient appointmentClient,
            HttpClient doctorClient,
            HttpClient patientRecordClient,
            IBillingRepository billingRepository,
            IBillingItemRepository billingItemRepository)
        {
            _userManagementClient = userManagementClient;
            _appointmentClient = appointmentClient;
            _doctorClient = doctorClient;
            _patientRecordClient = patientRecordClient;
            _billingRepository = billingRepository;
            _billingItemRepository = billingItemRepository;
        }


        public BillingsService(IBillingRepository billingRepository, IBillingItemRepository billingItemRepository)
        {
            _billingRepository = billingRepository;
            _billingItemRepository = billingItemRepository;
        }

        public async Task<IEnumerable<BillingReadDTO>> GetAllBillingsAsync()
        {
            var billings = await _billingRepository.GetAllBillingsAsync();
            var billingDTOs = billings.Select(b => new BillingReadDTO
            {
                BillingId = b.BillingId,
                PatientId = b.PatientId,
                Amount = b.Amount,
                BillingDate = b.BillingDate,
                PaymentStatus = b.PaymentStatus,
                PaymentMethod = b.PaymentMethod,
                CreatedAt = b.CreatedAt,
                UpdatedAt = b.UpdatedAt,
                BillingItems = b.BillingItems.Select(bi => new BillingItemDTO
                {
                    Description = bi.Description,
                    Cost = bi.Cost,
                    Quantity = bi.Quantity,
                    Total = bi.Total
                }).ToList()
            });

            return billingDTOs;
        }

        public async Task<BillingReadDTO> GetBillingByIdAsync(int billingId)
        {
            if (billingId <= 0)
                throw new ArgumentException("Invalid Billing ID.");

            var billing = await _billingRepository.GetBillingByIdAsync(billingId);
            if (billing == null)
                throw new KeyNotFoundException("Billing record not found.");

            var userResponse = await _userManagementClient.GetAsync($"api/User/{billing.PatientId}");
            if (!userResponse.IsSuccessStatusCode)
                throw new Exception("Failed to fetch user details from the User Management service.");

            var appointmentResponse = await _appointmentClient.GetAsync($"api/Appointment/{billing.PatientId}");
            if (!appointmentResponse.IsSuccessStatusCode)
                throw new Exception("Failed to fetch appointment details.");

            var doctorResponse = await _doctorClient.GetAsync($"api/Doctor/{billing.PatientId}");
            if (!doctorResponse.IsSuccessStatusCode)
                throw new Exception("Failed to fetch doctor details.");

            var patientRecordResponse = await _patientRecordClient.GetAsync($"api/PatientRecord/{billing.PatientId}");
            if (!patientRecordResponse.IsSuccessStatusCode)
                throw new Exception("Failed to fetch patient record.");

            var billingDTO = new BillingReadDTO
            {
                BillingId = billing.BillingId,
                PatientId = billing.PatientId,
                Amount = billing.Amount,
                BillingDate = billing.BillingDate,
                PaymentStatus = billing.PaymentStatus,
                PaymentMethod = billing.PaymentMethod,
                CreatedAt = billing.CreatedAt,
                UpdatedAt = billing.UpdatedAt,
                BillingItems = billing.BillingItems.Select(bi => new BillingItemDTO
                {
                    Description = bi.Description,
                    Cost = bi.Cost,
                    Quantity = bi.Quantity,
                    Total = bi.Total
                }).ToList()
            };

            return billingDTO;
        }

        public async Task AddBillingAsync(BillingCreateDTO billingCreateDTO)
        {
            var billing = new Billing
            {
                PatientId = billingCreateDTO.PatientId,
                Amount = billingCreateDTO.Amount,
                BillingDate = billingCreateDTO.BillingDate,
                PaymentStatus = billingCreateDTO.PaymentStatus,
                PaymentMethod = billingCreateDTO.PaymentMethod,
                BillingItems = billingCreateDTO.BillingItems.Select(biDTO => new BillingItem
                {
                    Description = biDTO.Description,
                    Cost = biDTO.Cost,
                    Quantity = biDTO.Quantity
                }).ToList()
            };

            await _billingRepository.AddBillingAsync(billing);
            await _billingRepository.SaveAsync();
        }

        public async Task UpdateBillingAsync(int billingId, BillingCreateDTO billingUpdateDTO)
        {
            var billing = await _billingRepository.GetBillingByIdAsync(billingId);
            if (billing == null) throw new Exception("Billing not found.");

            billing.PatientId = billingUpdateDTO.PatientId;
            billing.Amount = billingUpdateDTO.Amount;
            billing.BillingDate = billingUpdateDTO.BillingDate;
            billing.PaymentStatus = billingUpdateDTO.PaymentStatus;
            billing.PaymentMethod = billingUpdateDTO.PaymentMethod;
            billing.BillingItems = billingUpdateDTO.BillingItems.Select(biDTO => new BillingItem
            {
                Description = biDTO.Description,
                Cost = biDTO.Cost,
                Quantity = biDTO.Quantity
            }).ToList();

            await _billingRepository.UpdateBillingAsync(billing);
            await _billingRepository.SaveAsync();
        }

        public async Task DeleteBillingAsync(int billingId)
        {
            var billing = await _billingRepository.GetBillingByIdAsync(billingId);
            if (billing == null) throw new Exception("Billing not found.");

            await _billingRepository.DeleteBillingAsync(billingId);
            await _billingRepository.SaveAsync();
        }
    }
}
