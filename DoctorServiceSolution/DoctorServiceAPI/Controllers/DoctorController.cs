using DoctorServiceAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using DoctorServiceAPI.Models;
using DoctorServiceAPI.Models.DTOs;

namespace DoctorServiceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        private readonly IScheduleService _scheduleService;

        public DoctorController(IDoctorService doctorService, IScheduleService scheduleService)
        {
            _doctorService = doctorService;
            _scheduleService = scheduleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDoctors()
        {
            var doctors = await _doctorService.GetAllDoctorsAsync();
            if (doctors == null || !doctors.Any())
            {
                return NotFound("No doctors found.");
            }

            return Ok(doctors);
        }

        [HttpGet("{doctorId}")]
        public async Task<IActionResult> GetDoctorById(int doctorId)
        {
            var doctor = await _doctorService.GetDoctorByIdAsync(doctorId);
            if (doctor == null)
            {
                return NotFound($"Doctor with ID {doctorId} not found.");
            }

            return Ok(doctor);
        }

        [HttpPut("{doctorId}/availability")]
        public async Task<IActionResult> UpdateDoctorAvailability(int doctorId, [FromBody] UpdateAvailabilityDto availabilityDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var doctor = await _doctorService.GetDoctorByIdAsync(doctorId);
            if (doctor == null)
            {
                return NotFound($"Doctor with ID {doctorId} not found.");
            }

            var result = await _doctorService.UpdateDoctorAvailabilityAsync(doctorId, availabilityDto);
            if (result)
            {
                return Ok("Doctor availability updated successfully.");
            }

            return BadRequest("Failed to update doctor availability.");
        }

        [HttpGet("{doctorId}/schedules")]
        public async Task<IActionResult> GetDoctorSchedules(int doctorId)
        {
            var schedules = await _doctorService.GetDoctorSchedulesAsync(doctorId);
            if (schedules == null || !schedules.Any())
            {
                return NotFound("No schedules found for this doctor.");
            }

            return Ok(schedules);
        }

        [HttpPost("{doctorId}/schedules")]
        public async Task<IActionResult> AddSchedule(int doctorId, [FromBody] ScheduleDto scheduleDto)
        {
            if (scheduleDto.StartTime >= scheduleDto.EndTime)
            {
                return BadRequest("Start time must be earlier than end time.");
            }

            if (string.IsNullOrWhiteSpace(scheduleDto.DayOfWeek))
            {
                return BadRequest("Day of the week is required.");
            }

            var validDays = new[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            if (!validDays.Contains(scheduleDto.DayOfWeek))
            {
                return BadRequest("Invalid day of the week.");
            }

            var result = await _scheduleService.AddScheduleAsync(doctorId, scheduleDto);
            if (result)
            {
                return Ok("Schedule added successfully.");
            }

            return BadRequest("Failed to add schedule.");
        }

        [HttpPut("schedules/{scheduleId}")]
        public async Task<IActionResult> UpdateSchedule(int scheduleId, [FromBody] ScheduleDto scheduleDto)
        {
            if (scheduleDto.StartTime >= scheduleDto.EndTime)
            {
                return BadRequest("Start time must be earlier than end time.");
            }

            if (string.IsNullOrWhiteSpace(scheduleDto.DayOfWeek))
            {
                return BadRequest("Day of the week is required.");
            }

            var validDays = new[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            if (!validDays.Contains(scheduleDto.DayOfWeek))
            {
                return BadRequest("Invalid day of the week.");
            }

            var result = await _scheduleService.UpdateScheduleAsync(scheduleId, scheduleDto);
            if (result)
            {
                return Ok("Schedule updated successfully.");
            }

            return BadRequest("Failed to update schedule.");
        }

        [HttpDelete("schedules/{scheduleId}")]
        public async Task<IActionResult> DeleteSchedule(int scheduleId)
        {
            var result = await _scheduleService.DeleteScheduleAsync(scheduleId);
            if (result)
            {
                return Ok("Schedule deleted successfully.");
            }

            return BadRequest("Failed to delete schedule.");
        }
    }
}
