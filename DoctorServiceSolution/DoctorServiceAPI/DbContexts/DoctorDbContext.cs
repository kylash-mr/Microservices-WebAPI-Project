using DoctorServiceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DoctorServiceAPI.DbContexts
{
    public class DoctorDbContext : DbContext
    {
        public DoctorDbContext(DbContextOptions<DoctorDbContext> options) : base(options)
        {
        }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
    }
}
