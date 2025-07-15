using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RapPhim1.DTO.Ticket;
using RapPhim1.Service;

namespace RapPhim1.Controllers.Ticket
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrdersController(IOrderService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDTO dto)
        {
            try
            {
                var id = await _service.CreateOrderAsync(dto);
                return Ok(new { OrderId = id });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("{orderId}/pay")]
        public async Task<IActionResult> MarkAsPaid(Guid orderId)
        {
            var ok = await _service.MarkOrderAsPaidAsync(orderId);
            if (!ok) return BadRequest("Không thể cập nhật đơn.");
            return Ok("Đã thanh toán.");
        }

        [HttpDelete("{orderId}/cancel-if-expired")]
        public async Task<IActionResult> CancelIfExpired(Guid orderId)
        {
            var deleted = await _service.DeleteIfExpiredAsync(orderId);
            return deleted ? Ok("Đã xoá đơn hết hạn.") : BadRequest("Đơn chưa hết hạn hoặc không tồn tại.");
        }

        [HttpGet("revenue")]
        public async Task<IActionResult> GetRevenue([FromQuery] string type)
        {
            var data = await _service.GetRevenueAsync(type);
            return Ok(data);
        }
    }

}
