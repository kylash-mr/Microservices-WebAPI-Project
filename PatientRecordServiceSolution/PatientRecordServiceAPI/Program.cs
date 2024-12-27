
using Microsoft.EntityFrameworkCore;
using PatientRecordServiceAPI.DbContexts;
using PatientRecordServiceAPI.Repositories;
using PatientRecordServiceAPI.Services;

namespace PatientRecordServiceAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            #region DbContext
            builder.Services.AddDbContext<PatientDbContext>(options =>
             options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            #endregion
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped<IPatientRepository, PatientRepository>();
            builder.Services.AddScoped<IMedicalRecordsRepository, MedicalRecordsRepository>();
            builder.Services.AddScoped<IPatientService, PatientService>();
            builder.Services.AddScoped<IMedicalRecordService, MedicalRecordService>();

            #region HttpClients
            builder.Services.AddHttpClient("UserManagementService", client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["Services:UserManagement"]);
            });

            builder.Services.AddHttpClient("AppointmentService", client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["Services:AppointmentService"]);
            });

            builder.Services.AddHttpClient("DoctorService", client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["Services:DoctorService"]);
            });
            #endregion
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
