using DoctorServiceAPI.Models;

namespace DoctorServiceAPI.Repositories
{
    public interface IScheduleRepository
    {
        Task<IEnumerable<Schedule>> GetSchedulesByDoctorIdAsync(int doctorId);
        Task<Schedule?> GetScheduleByIdAsync(int scheduleId);
        Task<bool> AddScheduleAsync(Schedule schedule);
        Task<bool> UpdateScheduleAsync(Schedule schedule);
        Task<bool> DeleteScheduleAsync(int scheduleId);
        Task<bool> SaveAsync();
    }
}
