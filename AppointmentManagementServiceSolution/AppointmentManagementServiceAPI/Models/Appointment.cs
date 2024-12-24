namespace AppointmentManagementServiceAPI.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public string PatientId { get; set; } = string.Empty;
        public string DoctorId { get; set; } = string.Empty;
        public DateTime AppointmentDate { get; set; }
        public string Notes { get; set; } = string.Empty;
        public string Status { get; set; }=string.Empty;
    }

}
