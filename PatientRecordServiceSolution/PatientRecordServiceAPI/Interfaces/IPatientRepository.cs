using PatientRecordServiceAPI.Models;

namespace PatientRecordServiceAPI.Repositories
{
    public interface IPatientRepository : IRepository<Patient>
    {
        Task<Patient?> GetPatientWithRecordsAsync(int patientId);
    }
}
