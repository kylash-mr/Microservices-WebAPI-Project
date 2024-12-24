using AppointmentManagementServiceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AppointmentManagementServiceAPI.DbContexts
{
    public class AppointmentDbContext : DbContext
    {
        public AppointmentDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Appointment> Appointments { get; set; }
    }
}
