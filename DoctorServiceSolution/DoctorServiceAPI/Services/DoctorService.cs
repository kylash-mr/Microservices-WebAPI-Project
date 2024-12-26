using DoctorServiceAPI.Models.DTOs;
using DoctorServiceAPI.Interfaces;
using DoctorServiceAPI.Repositories;

namespace DoctorServiceAPI.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IUserService _userService;

        public DoctorService(IDoctorRepository doctorRepository, IScheduleRepository scheduleRepository, IUserService userService)
        {
            _doctorRepository = doctorRepository;
            _scheduleRepository = scheduleRepository;
            _userService = userService;
        }

        public async Task<IEnumerable<DoctorDto>> GetAllDoctorsAsync()
        {
            var doctors = await _doctorRepository.GetAllAsync();

            return doctors.Select(d => new DoctorDto
            {
                Name = d.Name,
                Specialty = d.Specialty,
                IsAvailable = d.IsAvailable
            }).ToList();
        }

        public async Task<DoctorDto?> GetDoctorByIdAsync(int doctorId)
        {
            var doctor = await _doctorRepository.GetByIdAsync(doctorId);
            if (doctor == null)
            {
                return null;
            }

            return new DoctorDto
            {
                Name = doctor.Name,
                Specialty = doctor.Specialty,
                IsAvailable = doctor.IsAvailable
            };
        }

        public async Task<bool> UpdateDoctorAvailabilityAsync(int doctorId, UpdateAvailabilityDto availabilityDto)
        {
            
            var isDoctor = await _userService.ValidateDoctorUserAsync(doctorId);
            if (!isDoctor) return false;

            var result = await _doctorRepository.UpdateAvailabilityAsync(doctorId, availabilityDto.IsAvailable);
            return result && await _doctorRepository.SaveAsync();
        }

        public async Task<IEnumerable<ScheduleDto>> GetDoctorSchedulesAsync(int doctorId)
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
    }
}
