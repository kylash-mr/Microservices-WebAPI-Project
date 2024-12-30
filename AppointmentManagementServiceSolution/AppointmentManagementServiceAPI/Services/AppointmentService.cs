using AppointmentManagementServiceAPI.Interfaces;
using AppointmentManagementServiceAPI.Models;
using AppointmentManagementServiceAPI.Models.DTOs;
using AppointmentManagementServiceAPI.Repositories;

namespace AppointmentManagementServiceAPI.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IUserService _userService;

        public AppointmentService(IAppointmentRepository repository,IUserService userService)
        {
            _appointmentRepository = repository;
            _userService = userService;
        }
        public async Task<bool> CancelAppointmentAsync(int id)
        {
            var appointment = await _appointmentRepository.GetAppointmentByIdAsync(id);
            if (appointment == null)
            {
                throw new KeyNotFoundException("Appointment not found");
            }

            appointment.Status = "Cancelled";

            var result = await _appointmentRepository.UpdateAppointmentAsync(appointment);
            return result != null;
        }

        public async Task<AppointmentDTO> CreateAppointmentAsync(AppointmentDTO AppointmentDTO)
        {
            if (!await _userService.IsPatientAsync(AppointmentDTO.PatientId))
            {
                throw new ArgumentException("Invalid patient ID");
            }

            if (!await _userService.IsDoctorAsync(AppointmentDTO.DoctorId))
            {
                throw new ArgumentException("Invalid doctor ID");
            }
            var appointment = new Appointment
            {
                PatientId = AppointmentDTO.PatientId,
                DoctorId = AppointmentDTO.DoctorId,
                AppointmentDate = AppointmentDTO.AppointmentDate,
                Notes = AppointmentDTO.Notes,
                Status = "Booked"
            };

            var createdAppointment = await _appointmentRepository.AddAppointmentAsync(appointment);

            var createdAppointmentDTO = new AppointmentDTO
            {
                Id = createdAppointment.Id,
                PatientId = createdAppointment.PatientId,
                DoctorId = createdAppointment.DoctorId,
                AppointmentDate = createdAppointment.AppointmentDate,
                Notes = createdAppointment.Notes,
                Status = createdAppointment.Status
            };

            return createdAppointmentDTO;
        }

        public async Task<AppointmentDTO> GetAppointmentByIdAsync(int id)
        {
            var appointment = await _appointmentRepository.GetAppointmentByIdAsync(id);
            if (appointment == null)
            {
                throw new KeyNotFoundException("Appointment not found");
            }

            return new AppointmentDTO
            {
                Id = appointment.Id,
                PatientId = appointment.PatientId,
                DoctorId = appointment.DoctorId,
                AppointmentDate = appointment.AppointmentDate,
                Notes = appointment.Notes,
                Status = appointment.Status
            };
        }

        public async Task<IEnumerable<AppointmentDTO>> GetAppointmentsByPatientIdAsync(string patientId)
        {
            var appointments = await _appointmentRepository.GetAppointmentsByPatientIdAsync(patientId);
            return appointments.Select(a => new AppointmentDTO
            {
                Id = a.Id,
                PatientId = a.PatientId,
                DoctorId = a.DoctorId,
                AppointmentDate = a.AppointmentDate,
                Notes = a.Notes,
                Status = a.Status
            });
        }

        public async Task<AppointmentDTO> UpdateAppointmentAsync(int id, AppointmentDTO AppointmentDTO)
        {
            var existingAppointment = await _appointmentRepository.GetAppointmentByIdAsync(id);
            if (existingAppointment == null)
            {
                throw new KeyNotFoundException("Appointment not found");
            }

            existingAppointment.AppointmentDate = AppointmentDTO.AppointmentDate;
            existingAppointment.Notes = AppointmentDTO.Notes;

            var updatedAppointment = await _appointmentRepository.UpdateAppointmentAsync(existingAppointment);

            return new AppointmentDTO
            {
                Id = updatedAppointment.Id,
                PatientId = updatedAppointment.PatientId,
                DoctorId = updatedAppointment.DoctorId,
                AppointmentDate = updatedAppointment.AppointmentDate,
                Notes = updatedAppointment.Notes,
                Status = updatedAppointment.Status
            };
        }
    }
}
