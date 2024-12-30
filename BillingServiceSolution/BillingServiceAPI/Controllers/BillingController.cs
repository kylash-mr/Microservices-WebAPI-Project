using BillingService.DTOs;
using BillingServiceAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BillingServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillingController : ControllerBase
    {
        private readonly IBillingService _billingService;

        public BillingController(IBillingService billingService)
        {
            _billingService = billingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBillings()
        {
            var billings = await _billingService.GetAllBillingsAsync();
            return Ok(billings);
        }

        [HttpGet("{billingId}")]
        public async Task<IActionResult> GetBillingById(int billingId)
        {
            var billing = await _billingService.GetBillingByIdAsync(billingId);
            if (billing == null)
            {
                return NotFound();
            }
            return Ok(billing);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBilling([FromBody] BillingCreateDTO billingCreateDTO)
        {
            if (billingCreateDTO == null)
            {
                return BadRequest("Billing data is null");
            }

            await _billingService.AddBillingAsync(billingCreateDTO);
            return Ok(new { message = "Billing created successfully", data = billingCreateDTO });
        }


        [HttpPut("{billingId}")]
        public async Task<IActionResult> UpdateBilling(int billingId, [FromBody] BillingCreateDTO billingUpdateDTO)
        {
            if (billingUpdateDTO == null)
            {
                return BadRequest("Billing data is null");
            }

            await _billingService.UpdateBillingAsync(billingId, billingUpdateDTO);
            return NoContent();
        }

        [HttpDelete("{billingId}")]
        public async Task<IActionResult> DeleteBilling(int billingId)
        {
            await _billingService.DeleteBillingAsync(billingId);
            return NoContent();
        }
    }
}
