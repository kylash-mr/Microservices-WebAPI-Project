using AppointmentManagementServiceAPI.Models;

namespace AppointmentManagementServiceAPI.Interfaces
{
    public interface IAppointmentRepository
    {

        Task<Appointment> AddAppointmentAsync(Appointment appointment);
        Task<Appointment> GetAppointmentByIdAsync(int id);
        Task<IEnumerable<Appointment>> GetAppointmentsByPatientIdAsync(string patientId);
        Task<Appointment> UpdateAppointmentAsync(Appointment appointment);
        Task<bool> DeleteAppointmentAsync(int id);

    }

}
