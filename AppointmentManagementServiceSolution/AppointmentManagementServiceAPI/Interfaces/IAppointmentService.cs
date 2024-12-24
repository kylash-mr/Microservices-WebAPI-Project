using AppointmentManagementServiceAPI.Models.DTOs;

namespace AppointmentManagementServiceAPI.Interfaces
{
    public interface IAppointmentService
    {
        Task<AppointmentDTO> CreateAppointmentAsync(AppointmentDTO appointmentDto);
        Task<AppointmentDTO> GetAppointmentByIdAsync(int id);
        Task<IEnumerable<AppointmentDTO>> GetAppointmentsByPatientIdAsync(string patientId);
        Task<AppointmentDTO> UpdateAppointmentAsync(int id, AppointmentDTO appointmentDto);
        Task<bool> CancelAppointmentAsync(int id);
    }
}
