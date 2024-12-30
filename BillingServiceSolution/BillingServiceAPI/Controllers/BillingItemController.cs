using BillingService.DTOs;
using BillingServiceAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BillingServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillingItemController : ControllerBase
    {
        private readonly IBillingItemService _billingItemService;

        public BillingItemController(IBillingItemService billingItemService)
        {
            _billingItemService = billingItemService;
        }

        [HttpGet("{billingId}")]
        public async Task<IActionResult> GetBillingItemsByBillingId(int billingId)
        {
            var billingItems = await _billingItemService.GetBillingItemsByBillingIdAsync(billingId);
            if (billingItems == null || !billingItems.Any())
            {
                return NotFound();
            }
            return Ok(billingItems);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBillingItem([FromBody] BillingItemDTO billingItemDTO)
        {
            if (billingItemDTO == null)
            {
                return BadRequest("Billing item data is null");
            }

            await _billingItemService.AddBillingItemAsync(billingItemDTO);
            return Ok(new { message = "Billing created successfully", data = billingItemDTO });
        }

        [HttpPut("{billingItemId}")]
        public async Task<IActionResult> UpdateBillingItem(int billingItemId, [FromBody] BillingItemDTO billingItemDTO)
        {
            if (billingItemDTO == null)
            {
                return BadRequest("Billing item data is null");
            }

            await _billingItemService.UpdateBillingItemAsync(billingItemId, billingItemDTO);
            return NoContent();
        }

        [HttpDelete("{billingItemId}")]
        public async Task<IActionResult> DeleteBillingItem(int billingItemId)
        {
            await _billingItemService.DeleteBillingItemAsync(billingItemId);
            return NoContent();
        }
    }
}
