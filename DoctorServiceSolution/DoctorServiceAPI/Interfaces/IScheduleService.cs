using DoctorServiceAPI.Models.DTOs;

namespace DoctorServiceAPI.Interfaces
{
    public interface IScheduleService
    {
        Task<IEnumerable<ScheduleDto>> GetSchedulesByDoctorIdAsync(int doctorId);
        Task<bool> AddScheduleAsync(int doctorId, ScheduleDto scheduleDto);
        Task<bool> UpdateScheduleAsync(int scheduleId, ScheduleDto scheduleDto);
        Task<bool> DeleteScheduleAsync(int scheduleId);
    }
}
