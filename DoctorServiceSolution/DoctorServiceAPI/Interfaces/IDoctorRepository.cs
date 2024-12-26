using DoctorServiceAPI.Models;

namespace DoctorServiceAPI.Repositories
{
    public interface IDoctorRepository
    {
        Task<IEnumerable<Doctor>> GetAllAsync();
        Task<Doctor?> GetByIdAsync(int doctorId);
        Task<bool> UpdateAvailabilityAsync(int doctorId, bool isAvailable);
        Task<bool> SaveAsync();
    }
}
