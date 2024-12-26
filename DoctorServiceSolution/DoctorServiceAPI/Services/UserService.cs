using DoctorServiceAPI.DbContexts;
using DoctorServiceAPI.Interfaces;
using DoctorServiceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DoctorServiceAPI.Services
{
    public class UserService : IUserService
    {
        private readonly DoctorDbContext _context;

        public UserService(DoctorDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ValidateDoctorUserAsync(int doctorId)
        {
            var doctor = await _context.Doctors
                .Where(u => u.DoctorId == doctorId)
                .FirstOrDefaultAsync();

            return doctor != null;
        }

    }
}
