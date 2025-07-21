using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RapPhim1.Service;

namespace RapPhim1.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/admin/seat-types")]
    public class AdminSeatTypeController : ControllerBase
    {
        private readonly ISeatTypeService _seatTypeService;
        public AdminSeatTypeController(ISeatTypeService seatTypeService) => _seatTypeService = seatTypeService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var seatTypes = await _seatTypeService.GetAllAsync();
            return Ok(seatTypes);
        }
    }
}
