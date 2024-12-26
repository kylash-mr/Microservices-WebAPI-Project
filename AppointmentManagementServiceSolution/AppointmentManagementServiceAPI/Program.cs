
using AppointmentManagementServiceAPI.DbContexts;
using AppointmentManagementServiceAPI.Interfaces;
using AppointmentManagementServiceAPI.Repositories;
using AppointmentManagementServiceAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

namespace AppointmentManagementServiceAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddHttpClient<IUserService, UserValidationService>();

            #region DbContext
            builder.Services.AddDbContext<AppointmentDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            #endregion
            #region Authentication and Authorization
            // Add authentication and authorization
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = builder.Configuration["Authentication:Authority"];
                    options.Audience = builder.Configuration["Authentication:Audience"]; 
                    options.RequireHttpsMetadata = false;  // Set to true for production
                });
            #endregion
            #region Repositories
            builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            #endregion

            #region Services
            builder.Services.AddScoped<IAppointmentService, AppointmentService>();
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
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
