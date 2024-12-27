using PatientRecordServiceAPI.Models;
using PatientRecordServiceAPI.Repositories;
using System.Net.Http;
using System.Text.Json;

namespace PatientRecordServiceAPI.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly HttpClient _httpClient;

        public PatientService(IPatientRepository patientRepository, HttpClient httpClient)
        {
            _patientRepository = patientRepository;
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Patient>> GetAllPatientsAsync()
        {
            return await _patientRepository.GetAllAsync();
        }

        public async Task<Patient?> GetPatientByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid patient ID");

            var patient = await _patientRepository.GetByIdAsync(id);
            if (patient == null)
                throw new KeyNotFoundException($"Patient with ID {id} not found");

            return patient;
        }

        public async Task<Patient?> GetPatientWithRecordsAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid patient ID");

            var patient = await _patientRepository.GetPatientWithRecordsAsync(id);
            if (patient == null)
                throw new KeyNotFoundException($"Patient with ID {id} not found");

            return patient;
        }

        public async Task AddPatientAsync(Patient patient)
        {
            if (patient == null)
                throw new ArgumentNullException(nameof(patient), "Patient object cannot be null");

            if (string.IsNullOrWhiteSpace(patient.PatientName))
                throw new ArgumentException("Patient name is required");

            if (string.IsNullOrWhiteSpace(patient.Email))
                throw new ArgumentException("Email is required");

            if (string.IsNullOrWhiteSpace(patient.PhoneNumber))
                throw new ArgumentException("Phone number is required");

            await _patientRepository.AddAsync(patient);
        }

        public async Task UpdatePatientAsync(Patient patient)
        {
            if (patient == null)
                throw new ArgumentNullException(nameof(patient), "Patient object cannot be null");

            if (patient.PatientId <= 0)
                throw new ArgumentException("Invalid patient ID");

            var existingPatient = await _patientRepository.GetByIdAsync(patient.PatientId);
            if (existingPatient == null)
                throw new KeyNotFoundException($"Patient with ID {patient.PatientId} not found");

            await _patientRepository.UpdateAsync(patient);
        }

        public async Task DeletePatientAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid patient ID");

            var existingPatient = await _patientRepository.GetByIdAsync(id);
            if (existingPatient == null)
                throw new KeyNotFoundException($"Patient with ID {id} not found");

            await _patientRepository.DeleteAsync(id);
        }

        public async Task<Patient?> GetPatientWithDetailsAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid patient ID");

            var patient = await _patientRepository.GetByIdAsync(id);
            if (patient == null)
                throw new KeyNotFoundException($"Patient with ID {id} not found");

            var appointmentResponse = await _httpClient.GetAsync($"http://appointment-service/api/appointments/patient/{id}");
            if (!appointmentResponse.IsSuccessStatusCode)
                throw new HttpRequestException("Failed to fetch appointments.");

            var appointmentContent = await appointmentResponse.Content.ReadAsStringAsync();
            var appointments = JsonSerializer.Deserialize<List<object>>(appointmentContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            var doctorResponse = await _httpClient.GetAsync($"http://doctor-service/api/doctors/{id}");
            if (!doctorResponse.IsSuccessStatusCode)
                throw new HttpRequestException("Failed to fetch doctor details.");

            var doctorContent = await doctorResponse.Content.ReadAsStringAsync();
            var doctorDetails = JsonSerializer.Deserialize<object>(doctorContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            var userResponse = await _httpClient.GetAsync($"http://usermanagement-service/api/users/{id}");
            if (!userResponse.IsSuccessStatusCode)
                throw new HttpRequestException("Failed to fetch user details.");

            var userContent = await userResponse.Content.ReadAsStringAsync();
            var userDetails = JsonSerializer.Deserialize<object>(userContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            var patientDetails = new
            {
                Patient = patient,
                Appointments = appointments,
                DoctorDetails = doctorDetails,
                UserDetails = userDetails
            };

            return patient;  
        }
    }
}
