using Microsoft.EntityFrameworkCore;
using UserManagementServiceAPI.Models;

namespace UserManagementServiceAPI.DbContexts
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    UserName = "John",
                    Password = "JohnDoe123",
                    PhoneNumber = "9422221230",
                    Email = "johndoe@gmail.com",
                    UserCity = "Chennai",
                    Role = "Patient"
                },
                new User
                {
                    UserId = 2,
                    UserName = "Alex",
                    Password = "AlexAdmin",
                    PhoneNumber = "9022296230",
                    Email = "dralex@apollo.com",
                    UserCity = "Chennai",
                    Role = "Doctor"
                }
                );
        }

    }
}
