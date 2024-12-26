
using DoctorServiceAPI.DbContexts;
using DoctorServiceAPI.Interfaces;
using DoctorServiceAPI.Repositories;
using DoctorServiceAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace DoctorServiceAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            #region Repositories
            builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
            builder.Services.AddScoped<IScheduleRepository, ScheduleRepository>();
            #endregion

            #region Services
            builder.Services.AddScoped<IDoctorService, DoctorService>();
            builder.Services.AddScoped<IScheduleService, ScheduleService>();
            builder.Services.AddScoped<IUserService, UserService>();
            #endregion

            builder.Services.AddHttpClient<IUserService, UserService>(client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["ApiUrls:UserManagementApiUrl"]);
            });
            builder.Services.AddHttpClient<IAppointmentService, AppointmentService>(client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["ApiUrls:AppointmentApiUrl"]);
            });


            #region Context
            builder.Services.AddDbContext<DoctorDbContext>(options =>            
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            #endregion


            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
