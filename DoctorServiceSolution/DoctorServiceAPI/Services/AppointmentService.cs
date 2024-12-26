using DoctorServiceAPI.Interfaces;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

namespace DoctorServiceAPI.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly HttpClient _httpClient;

        public AppointmentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> IsDoctorAvailableAsync(int doctorId, DateTime startTime, DateTime endTime)
        {
            var queryParams = $"?doctorId={doctorId}&startTime={startTime:yyyy-MM-ddTHH:mm:ss}&endTime={endTime:yyyy-MM-ddTHH:mm:ss}";

            var response = await _httpClient.GetAsync($"/api/Appointment/{queryParams}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                if (content == "true")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }
    }
}
