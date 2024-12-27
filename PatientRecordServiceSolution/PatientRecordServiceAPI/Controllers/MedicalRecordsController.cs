using Microsoft.AspNetCore.Mvc;
using PatientRecordServiceAPI.Models;
using PatientRecordServiceAPI.Services;

namespace PatientRecordServiceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicalRecordsController : ControllerBase
    {
        private readonly IMedicalRecordService _medicalRecordService;

        public MedicalRecordsController(IMedicalRecordService medicalRecordService)
        {
            _medicalRecordService = medicalRecordService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRecords()
        {
            var records = await _medicalRecordService.GetAllRecordsAsync();
            return Ok(records);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecordById(int id)
        {
            try
            {
                var record = await _medicalRecordService.GetRecordByIdAsync(id);
                return Ok(record);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("patient/{patientId}")]
        public async Task<IActionResult> GetRecordsByPatientId(int patientId)
        {
            try
            {
                var records = await _medicalRecordService.GetRecordsByPatientIdAsync(patientId);
                return Ok(records);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddRecord([FromBody] MedicalRecords record)
        {
            try
            {
                await _medicalRecordService.AddRecordAsync(record);
                return CreatedAtAction(nameof(GetRecordById), new { id = record.RecordId }, record);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRecord(int id, [FromBody] MedicalRecords record)
        {
            try
            {
                record.RecordId = id; // Ensure the ID matches
                await _medicalRecordService.UpdateRecordAsync(record);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecord(int id)
        {
            try
            {
                await _medicalRecordService.DeleteRecordAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
