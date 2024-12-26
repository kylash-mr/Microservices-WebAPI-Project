namespace DoctorServiceAPI.Interfaces
{
    public interface IUserService
    {
        Task<bool> ValidateDoctorUserAsync(int doctorId);
    }
}
