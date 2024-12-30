
using BillingService.Repositories;
using BillingServiceAPI.DbContexts;
using BillingServiceAPI.Interfaces;
using BillingServiceAPI.Services;
using BillingsServiceAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace BillingServiceAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            #region Register DbContext
            builder.Services.AddDbContext<BillingDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            #endregion

            #region Register Repositories
            builder.Services.AddScoped<IBillingRepository, BillingRepository>();
            builder.Services.AddScoped<IBillingItemRepository, BillingItemRepository>();
            #endregion
            #region Register Services
            builder.Services.AddScoped<IBillingService, BillingsService>();
            builder.Services.AddScoped<IBillingItemService, BillingItemService>();
            #endregion

            builder.Services.AddSingleton<HttpClient>(new HttpClient
            {
                BaseAddress = new Uri(builder.Configuration["Services:UserManagement"])
            });
            builder.Services.AddSingleton<HttpClient>(new HttpClient
            {
                BaseAddress = new Uri(builder.Configuration["Services:Appointment"])
            });
            builder.Services.AddSingleton<HttpClient>(new HttpClient
            {
                BaseAddress = new Uri(builder.Configuration["Services:Doctor"])
            });
            builder.Services.AddSingleton<HttpClient>(new HttpClient
            {
                BaseAddress = new Uri(builder.Configuration["Services:PatientRecord"])
            });



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
