using Microsoft.EntityFrameworkCore;
using PatientRecordServiceAPI.Models;

namespace PatientRecordServiceAPI.DbContexts
{
    public class PatientDbContext : DbContext
    {
        public PatientDbContext(DbContextOptions<PatientDbContext> options) : base(options)
        {
        }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<MedicalRecords> MedicalRecords { get; set; }
       
    }
    
}
