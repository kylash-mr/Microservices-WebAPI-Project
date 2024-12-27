using PatientRecordServiceAPI.Models;

namespace PatientRecordServiceAPI.Repositories
{
    public interface IMedicalRecordsRepository : IRepository<MedicalRecords>
    {
        Task<IEnumerable<MedicalRecords>> GetRecordsByPatientIdAsync(int patientId);
    }
}
