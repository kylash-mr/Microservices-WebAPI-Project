using PatientRecordServiceAPI.Models;
using PatientRecordServiceAPI.Repositories;

namespace PatientRecordServiceAPI.Services
{
    public class MedicalRecordService : IMedicalRecordService
    {
        private readonly IMedicalRecordsRepository _medicalRecordsRepository;

        public MedicalRecordService(IMedicalRecordsRepository medicalRecordsRepository)
        {
            _medicalRecordsRepository = medicalRecordsRepository;
        }

        public async Task<IEnumerable<MedicalRecords>> GetAllRecordsAsync()
        {
            return await _medicalRecordsRepository.GetAllAsync();
        }

        public async Task<MedicalRecords?> GetRecordByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid record ID");

            var record = await _medicalRecordsRepository.GetByIdAsync(id);
            if (record == null)
                throw new KeyNotFoundException($"Medical record with ID {id} not found");

            return record;
        }

        public async Task<IEnumerable<MedicalRecords>> GetRecordsByPatientIdAsync(int patientId)
        {
            if (patientId <= 0)
                throw new ArgumentException("Invalid patient ID");

            return await _medicalRecordsRepository.GetRecordsByPatientIdAsync(patientId);
        }

        public async Task AddRecordAsync(MedicalRecords record)
        {
            if (record == null)
                throw new ArgumentNullException(nameof(record), "Medical record object cannot be null");

            if (string.IsNullOrWhiteSpace(record.Diagnosis))
                throw new ArgumentException("Diagnosis is required");

            await _medicalRecordsRepository.AddAsync(record);
        }

        public async Task UpdateRecordAsync(MedicalRecords record)
        {
            if (record == null)
                throw new ArgumentNullException(nameof(record), "Medical record object cannot be null");

            if (record.RecordId <= 0)
                throw new ArgumentException("Invalid record ID");

            var existingRecord = await _medicalRecordsRepository.GetByIdAsync(record.RecordId);
            if (existingRecord == null)
                throw new KeyNotFoundException($"Medical record with ID {record.RecordId} not found");

            await _medicalRecordsRepository.UpdateAsync(record);
        }

        public async Task DeleteRecordAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid record ID");

            var existingRecord = await _medicalRecordsRepository.GetByIdAsync(id);
            if (existingRecord == null)
                throw new KeyNotFoundException($"Medical record with ID {id} not found");

            await _medicalRecordsRepository.DeleteAsync(id);
        }
    }
}
