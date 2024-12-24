using AppointmentManagementServiceAPI.Interfaces;
using AppointmentManagementServiceAPI.Models;
using AppointmentManagementServiceAPI.Models.DTOs;

namespace AppointmentManagementServiceAPI.Services
{
    public class UserValidationService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly string _userManagementApiUrl;

        public UserValidationService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _userManagementApiUrl = configuration["UserManagementApiUrl"];
        }

        public async Task<bool> IsDoctorAsync(string userId)
        {
            var response = await _httpClient.GetAsync($"{_userManagementApiUrl}/api/users/{userId}");
            if (response.IsSuccessStatusCode)
            {
                var user = await response.Content.ReadFromJsonAsync<User>();
                return user.Role == "Doctor";
            }
            return false;
        }

        public async Task<bool> IsPatientAsync(string userId)
        {
            var response = await _httpClient.GetAsync($"{_userManagementApiUrl}/api/users/{userId}");
            if (response.IsSuccessStatusCode)
            {
                var user = await response.Content.ReadFromJsonAsync<User>();
                return user.Role == "Patient";
            }
            return false;
        }

        public async Task<bool> IsUserExistAsync(string userId)
        {
            var response = await _httpClient.GetAsync($"{_userManagementApiUrl}/api/users/{userId}");
            return response.IsSuccessStatusCode;
        }
    }
}
