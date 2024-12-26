using DoctorServiceAPI.Models.DTOs;

namespace DoctorServiceAPI.Interfaces
{
    public interface IDoctorService
    {
        Task<IEnumerable<DoctorDto>> GetAllDoctorsAsync();
        Task<DoctorDto?> GetDoctorByIdAsync(int doctorId);
        Task<bool> UpdateDoctorAvailabilityAsync(int doctorId, UpdateAvailabilityDto availabilityDto);
        Task<IEnumerable<ScheduleDto>> GetDoctorSchedulesAsync(int doctorId);
    }
}
