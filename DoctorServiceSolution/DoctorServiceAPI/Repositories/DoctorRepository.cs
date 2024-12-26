using DoctorServiceAPI.DbContexts;
using DoctorServiceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DoctorServiceAPI.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly DoctorDbContext _context;

        public DoctorRepository(DoctorDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Doctor>> GetAllAsync()
        {
            return await _context.Doctors
                                 .Include(d => d.Schedules)
                                 .ToListAsync();
        }

        public async Task<Doctor?> GetByIdAsync(int doctorId)
        {
            return await _context.Doctors
                                 .Include(d => d.Schedules)
                                 .FirstOrDefaultAsync(d => d.DoctorId == doctorId);
        }

        public async Task<bool> UpdateAvailabilityAsync(int doctorId, bool isAvailable)
        {
            var doctor = await _context.Doctors.FindAsync(doctorId);
            if (doctor == null) return false;

            doctor.IsAvailable = isAvailable;
            _context.Doctors.Update(doctor);
            return true;
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
