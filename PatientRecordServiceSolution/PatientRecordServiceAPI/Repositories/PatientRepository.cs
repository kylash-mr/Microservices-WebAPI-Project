using Microsoft.EntityFrameworkCore;
using PatientRecordServiceAPI.DbContexts;
using PatientRecordServiceAPI.Models;

namespace PatientRecordServiceAPI.Repositories
{
    public class PatientRepository : Repository<Patient>, IPatientRepository
    {
        public PatientRepository(PatientDbContext context) : base(context)
        {
        }

        public async Task<Patient?> GetPatientWithRecordsAsync(int patientId)
        {
            return await _context.Patients
                .Include(p => p.MedicalRecords)
                .FirstOrDefaultAsync(p => p.PatientId == patientId);
        }
    }
}
