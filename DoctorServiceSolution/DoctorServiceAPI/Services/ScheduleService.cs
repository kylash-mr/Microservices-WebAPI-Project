using DoctorServiceAPI.Interfaces;
using DoctorServiceAPI.Models;
using DoctorServiceAPI.Models.DTOs;
using DoctorServiceAPI.Repositories;

namespace DoctorServiceAPI.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IScheduleRepository _scheduleRepository;

        public ScheduleService(IScheduleRepository scheduleRepository)
        {
            _scheduleRepository = scheduleRepository;
        }

        public async Task<IEnumerable<ScheduleDto>> GetSchedulesByDoctorIdAsync(int doctorId)
        {
            var schedules = await _scheduleRepository.GetSchedulesByDoctorIdAsync(doctorId);

            return schedules.Select(s => new ScheduleDto
            {
                StartTime = s.StartTime,
                EndTime = s.EndTime,
                DayOfWeek = s.DayOfWeek,
                IsBooked = s.IsBooked
            }).ToList();
        }

        public async Task<bool> AddScheduleAsync(int doctorId, ScheduleDto scheduleDto)
        {
            var schedule = new Schedule
            {
                DoctorId = doctorId,
                StartTime = scheduleDto.StartTime,
                EndTime = scheduleDto.EndTime,
                DayOfWeek = scheduleDto.DayOfWeek
            };

            return await _scheduleRepository.AddScheduleAsync(schedule) && await _scheduleRepository.SaveAsync();
        }

        public async Task<bool> UpdateScheduleAsync(int scheduleId, ScheduleDto scheduleDto)
        {
            var schedule = await _scheduleRepository.GetScheduleByIdAsync(scheduleId);
            if (schedule == null) return false;

            schedule.StartTime = scheduleDto.StartTime;
            schedule.EndTime = scheduleDto.EndTime;
            schedule.DayOfWeek = scheduleDto.DayOfWeek;

            return await _scheduleRepository.UpdateScheduleAsync(schedule) && await _scheduleRepository.SaveAsync();
        }

        public async Task<bool> DeleteScheduleAsync(int scheduleId)
        {
            return await _scheduleRepository.DeleteScheduleAsync(scheduleId) && await _scheduleRepository.SaveAsync();
        }
    }
}
