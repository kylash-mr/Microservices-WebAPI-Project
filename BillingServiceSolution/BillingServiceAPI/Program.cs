
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
            // Register DbContext with dependency injection
            builder.Services.AddDbContext<BillingDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Register repositories
            builder.Services.AddScoped<IBillingRepository, BillingRepository>();
            builder.Services.AddScoped<IBillingItemRepository, BillingItemRepository>();

            // Register services
            builder.Services.AddScoped<IBillingService, BillingsService>();
            builder.Services.AddScoped<IBillingItemService, BillingItemService>();

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
