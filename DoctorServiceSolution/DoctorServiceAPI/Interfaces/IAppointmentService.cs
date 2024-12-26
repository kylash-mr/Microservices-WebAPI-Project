namespace DoctorServiceAPI.Interfaces
{
    public interface IAppointmentService
    {
        Task<bool> IsDoctorAvailableAsync(int doctorId, DateTime startTime, DateTime endTime);
    }
}
