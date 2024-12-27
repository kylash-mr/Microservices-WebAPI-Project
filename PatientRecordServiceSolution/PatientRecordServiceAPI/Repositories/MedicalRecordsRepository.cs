using Microsoft.EntityFrameworkCore;
using PatientRecordServiceAPI.DbContexts;
using PatientRecordServiceAPI.Models;

namespace PatientRecordServiceAPI.Repositories
{
    public class MedicalRecordsRepository : Repository<MedicalRecords>, IMedicalRecordsRepository
    {
        public MedicalRecordsRepository(PatientDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<MedicalRecords>> GetRecordsByPatientIdAsync(int patientId)
        {
            return await _context.MedicalRecords
                .Where(mr => mr.PatientId == int.Parse(patientId.ToString()))
                .ToListAsync();
        }
    }
}
