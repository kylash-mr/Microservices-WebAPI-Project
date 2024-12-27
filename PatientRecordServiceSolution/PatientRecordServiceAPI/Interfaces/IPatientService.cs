using PatientRecordServiceAPI.Models;

namespace PatientRecordServiceAPI.Services
{
    public interface IPatientService
    {
        Task<IEnumerable<Patient>> GetAllPatientsAsync();
        Task<Patient?> GetPatientByIdAsync(int id);
        Task<Patient?> GetPatientWithRecordsAsync(int id);
        Task AddPatientAsync(Patient patient);
        Task UpdatePatientAsync(Patient patient);
        Task DeletePatientAsync(int id);
    }
}
