using DoctorServiceAPI.DbContexts;
using DoctorServiceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DoctorServiceAPI.Repositories
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly DoctorDbContext _context;

        public ScheduleRepository(DoctorDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Schedule>> GetSchedulesByDoctorIdAsync(int doctorId)
        {
            return await _context.Schedules
                                 .Where(s => s.DoctorId == doctorId)
                                 .ToListAsync();
        }

        public async Task<Schedule?> GetScheduleByIdAsync(int scheduleId)
        {
            return await _context.Schedules.FindAsync(scheduleId);
        }

        public async Task<bool> AddScheduleAsync(Schedule schedule)
        {
            await _context.Schedules.AddAsync(schedule);
            return true;
        }

        public async Task<bool> UpdateScheduleAsync(Schedule schedule)
        {
            _context.Schedules.Update(schedule);
            return true;
        }

        public async Task<bool> DeleteScheduleAsync(int scheduleId)
        {
            var schedule = await _context.Schedules.FindAsync(scheduleId);
            if (schedule == null) return false;

            _context.Schedules.Remove(schedule);
            return true;
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
