using AppointmentManagementServiceAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using AppointmentManagementServiceAPI.Models.DTOs;

namespace AppointmentManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        // POST: api/appointments
        [HttpPost]
        public async Task<ActionResult<AppointmentDTO>> CreateAppointment(AppointmentDTO AppointmentDTO)
        {
            try
            {
                var createdAppointment = await _appointmentService.CreateAppointmentAsync(AppointmentDTO);
                return CreatedAtAction(nameof(GetAppointmentById), new { id = createdAppointment.Id }, createdAppointment);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/appointments/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentDTO>> GetAppointmentById(int id)
        {
            try
            {
                var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
                return Ok(appointment);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // GET: api/appointments/patient/{patientId}
        [HttpGet("patient/{patientId}")]
        public async Task<ActionResult<IEnumerable<AppointmentDTO>>> GetAppointmentsByPatientId(string patientId)
        {
            var appointments = await _appointmentService.GetAppointmentsByPatientIdAsync(patientId);
            return Ok(appointments);
        }

        // PUT: api/appointments/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<AppointmentDTO>> UpdateAppointment(int id, AppointmentDTO AppointmentDTO)
        {
            try
            {
                var updatedAppointment = await _appointmentService.UpdateAppointmentAsync(id, AppointmentDTO);
                return Ok(updatedAppointment);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // DELETE: api/appointments/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> CancelAppointment(int id)
        {
            try
            {
                var result = await _appointmentService.CancelAppointmentAsync(id);
                if (result)
                {
                    return NoContent(); 
                }
                return NotFound("Appointment not found");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
