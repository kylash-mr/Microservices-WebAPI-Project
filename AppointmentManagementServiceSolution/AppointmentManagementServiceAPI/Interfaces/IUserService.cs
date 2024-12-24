using AppointmentManagementServiceAPI.Models.DTOs;

namespace AppointmentManagementServiceAPI.Interfaces
{
    public interface IUserService
    {
        Task<bool> IsUserExistAsync(string userId);
        Task<bool> IsDoctorAsync(string userId);
        Task<bool> IsPatientAsync(string userId);
    }

}

