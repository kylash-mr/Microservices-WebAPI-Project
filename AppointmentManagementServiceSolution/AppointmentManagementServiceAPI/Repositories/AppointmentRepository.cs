using System.Runtime.CompilerServices;
using AppointmentManagementServiceAPI.DbContexts;
using AppointmentManagementServiceAPI.Interfaces;
using AppointmentManagementServiceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AppointmentManagementServiceAPI.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly AppointmentDbContext _dbContext;

        public AppointmentRepository(AppointmentDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Appointment> AddAppointmentAsync(Appointment appointment)
        {
            _dbContext.Appointments.Add(appointment);
            await _dbContext.SaveChangesAsync();
            return appointment;
        }

        public async Task<bool> DeleteAppointmentAsync(int id)
        {
            var appointment = await _dbContext.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return false;
            }

            _dbContext.Appointments.Remove(appointment);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Appointment> GetAppointmentByIdAsync(int id)
        {
            var appointment = await _dbContext.Appointments.FirstOrDefaultAsync(a => a.Id == id);
            if (appointment == null)
            {
                throw new Exception("Appointment not found");
            }
            return appointment;
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByPatientIdAsync(string patientId)
        {
            var appointment = await _dbContext.Appointments.Where(a => a.PatientId == patientId).ToListAsync();
            if (appointment == null)
            {
                throw new Exception("Appointment not found");
            }
            return appointment;
        }

        public async Task<Appointment> UpdateAppointmentAsync(Appointment appointment)
        {
            _dbContext.Appointments.Update(appointment);
            await _dbContext.SaveChangesAsync();
            return appointment;
        }
    }
}
