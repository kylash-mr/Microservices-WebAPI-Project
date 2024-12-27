using PatientRecordServiceAPI.Models;

namespace PatientRecordServiceAPI.Services
{
    public interface IMedicalRecordService
    {
        Task<IEnumerable<MedicalRecords>> GetAllRecordsAsync();
        Task<MedicalRecords?> GetRecordByIdAsync(int id);
        Task<IEnumerable<MedicalRecords>> GetRecordsByPatientIdAsync(int patientId);
        Task AddRecordAsync(MedicalRecords record);
        Task UpdateRecordAsync(MedicalRecords record);
        Task DeleteRecordAsync(int id);
    }
}
